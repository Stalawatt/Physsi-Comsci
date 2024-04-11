using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using static Physsi___Comsci_Project.SB_LOGIC;

namespace Physsi___Comsci_Project
{
    public class SB_LOGIC
    {
        
        public static class Items
        {
           public static List<Circle> Circles = new List<Circle>();

            
        }

        public class Circle
        {

            public List<Node> CircleNodes = new List<Node>();


            public int Radius;
            public int NodeAmount;
            public Vector2 MidPoint;


           
            
            public Vector2 CurrentForceInitial = new Vector2(0, Options_Settings.Gravity.Constant);


            

            public Circle(int radius, int nodeAmount, Vector2 midPoint)
            {
                
                Radius = radius;
                NodeAmount = nodeAmount;
                MidPoint = midPoint;

            }

            private double degToRad(double angle)
            {

                double radians = angle * Math.PI / 180;

                return radians;
            }
            
            public void createCircle(ContentManager Content)
            {
                int degreesInterval = 360 / NodeAmount;
                double currentDegrees = 0;
                int currentId = 0;
                Texture2D Nodetexture = Content.Load<Texture2D>("SoftBody_Editor/NodeTexture");


                while (currentDegrees < 360)
                {
                    CircleNodes.Add(new Node(new Vector2((float)(MidPoint.X + (Radius * Math.Sin(degToRad(currentDegrees)))), (float)(MidPoint.Y + (Radius * Math.Cos( degToRad(currentDegrees))))), currentId, SoftBody_Editor.SpawnCircle.Sprite, 13 , 1f));
                    currentId++;
                    currentDegrees += degreesInterval;
                }
                

                foreach (Node thisnode in CircleNodes)
                {
                    foreach (Node othernode in CircleNodes)
                    {
                        if (othernode != thisnode)
                        {
                            thisnode.springList.Add(new Spring(thisnode, othernode , 0.01f, 1f) );
                        }
                    }
                }

                
            }

            public void drawCircle(SpriteBatch _spriteBatch)
            {
                foreach (Node node in CircleNodes)
                {
                    _spriteBatch.Draw(node.Sprite, node.Position, Color.White);
                }
            }



            // Frame update logic for the circle items

            public void updateCircle() 
            {

                // update force, velocity, acceleration and position

                foreach( Node node in CircleNodes)
                {
                    updateForce(node);
                   
                }

                foreach (Node node in CircleNodes)
                {
                    
                    updateVelocity(node);
                    
                }

                foreach (Node node in CircleNodes)
                {

                    updatePosition(node);
                }

                foreach (Node node in CircleNodes)
                {
                    node.resetForce();

                }
            }

            public void updateForce(Node node)
            {
                
                node.updateForce();
                
            }
           
            public void updateVelocity(Node node)
            {
                node.updateVelocity();
            }

            public void updatePosition(Node node)
            {
                node.updatePosition(this);
            }

        }


        public class Node
        {

            // Circular nodes to show the particles that interact with the world

            public Vector2 Position;
            public Vector2 InitialPosition;
            public int ID;

            public List<Spring> springList = new List<Spring>();

            
            public Vector2 Velocity = new Vector2(0,0);

            public float Mass;

            public Vector2 nearestFloorVector;


            public Vector2 CurrentForce;
            

            public Texture2D Sprite;
            public int Radius;

            public Node(Vector2 position, int id,  Texture2D sprite, int radius, float mass)
            { 

                Position = position;
                InitialPosition = position;
                ID = id;
                Mass = mass;
                Sprite = sprite;
                Radius = radius;

            }


            public void updateForce()
            {
                
                CurrentForce.Y += Options_Settings.Gravity.Constant * Mass;
                
                foreach(Spring spring in springList)
                {
                    CurrentForce += spring.FindForce();
                }
            }
            public void resetForce()
            {
                CurrentForce = Vector2.Zero;
            }
            public void addForce(Vector2 Force)
            {
                CurrentForce += Force;
            }

            public void updateVelocity()
            {
                Velocity += (CurrentForce * deltaTime.GetDeltaTime()) / Mass;
                
            }

            public void updatePosition(Circle circle)
            {
                Position += Velocity * deltaTime.GetDeltaTime();
                collisionCheck(circle);
            }
         
            public void collisionCheck(Circle circle)
            {
                if (Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Radius * 2 )
                {
                    Vector2 nearestFloorPos = new Vector2(Position.X, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Radius * 2);
                    nearestFloorVector = ( Position - nearestFloorPos ) / Vector2.Distance(nearestFloorPos, Position);

                    Velocity = Velocity - 2 * nearestFloorVector* (Vector2.Dot(Velocity, nearestFloorVector));
                }
                
                foreach (Node node in circle.CircleNodes)
                {
                    if (node == this || node.Position == this.Position)
                    {
                        return;
                    }
                    else 
                    {
                        float distance = pythagorasNodeDistance(node, this);

                        if (distance < 25) // if intersecting
                        {
                            Vector2 PosA = this.Position + new Vector2(12.5f, 12.5f); // finds centre of this node
                            Vector2 PosB = node.Position + new Vector2(12.5f, 12.5f); // finds centre of other node

                            Vector2 NormalisedDirVectorA = Vector2.Normalize(PosB - PosA) ;
                            Vector2 NormalisedDirVectorB = -1 * Vector2.Normalize(PosB - PosA);


                            this.Position = PosB - NormalisedDirVectorA * (25 - distance)  / 2f;
                            node.Position = PosA - NormalisedDirVectorB * (25 - distance) / 2f ;

                            this.Velocity = this.Velocity - 2 * NormalisedDirVectorA * (Vector2.Dot(this.Velocity, NormalisedDirVectorA));
                            node.Velocity = node.Velocity - 2 * NormalisedDirVectorB * (Vector2.Dot(this.Velocity, NormalisedDirVectorB));

                            
                        }
                    }

                    
                }
            }

            private float pythagorasNodeDistance(Node nodeA, Node nodeB)
            {
                Vector2 PosA = nodeA.Position;
                Vector2 PosB = nodeB.Position;

                Vector2 AtoB = PosB - PosA;

                float Distance = (float)(Math.Sqrt((double)(AtoB.X * AtoB.X + AtoB.Y * AtoB.Y)));

                return Distance;
            }


        }







        public class Spring
        {

            public Spring(Node node_a, Node node_b, float springconst, float dampingconst)
            {
                nodeA = node_a;
                nodeB = node_b;
                springConst = springconst;
                dampingConst = dampingconst;

                initialLength = pythagorasNodeDistance(nodeA, nodeB);
            }



            Node nodeA;
            Node nodeB;

            float springConst;
            float dampingConst;

            float initialLength;


            private float pythagorasNodeDistance(Node nodeA , Node nodeB)
            {
                Vector2 PosA = nodeA.Position;
                Vector2 PosB = nodeB.Position;

                Vector2 AtoB = PosB - PosA;

                float Distance = Vector2.Distance(AtoB, PosA);

                return Distance;
            }


            public Vector2 FindForce()
            {
               

                float distanceBetweenNodes = pythagorasNodeDistance(nodeA, nodeB);

                if (distanceBetweenNodes == initialLength)
                {

                    return Vector2.Zero;
                }

                float SpringForce = (distanceBetweenNodes - initialLength) * springConst;

                Vector2 NormalisedDirectionVector = ( nodeB.Position - nodeA.Position ) / distanceBetweenNodes;

                Vector2 VelocityDifference = nodeB.Velocity - nodeA.Velocity;

                float Dotproduct = Vector2.Dot(NormalisedDirectionVector, VelocityDifference);

                float DampingForce = Dotproduct * dampingConst;

                float TotalForce = SpringForce + DampingForce;

                Vector2 returnForce = TotalForce * NormalisedDirectionVector;

                
                
                return returnForce;
            }

        }

        
        public static void createCircleObj(ContentManager Content)
        {
            Circle newCircle = new Circle(200, 2, new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 , GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2));
            newCircle.createCircle(Content);

            Items.Circles.Add(newCircle);

        }


        // Below this is for the scene player's logic


        public static void Draw_SB(SpriteBatch _spriteBatch)
        {
            deltaTime.Start();
            foreach (Circle Circle in Items.Circles)
            {
                Circle.updateCircle();
                Circle.drawCircle(_spriteBatch);
                
            }
            deltaTime.End();
        }


        public static void resetCircleNodes()
        {
            foreach (Circle Circle in Items.Circles)
            {
                foreach(Node node in Circle.CircleNodes)
                {
                    node.Position = node.InitialPosition;
                    node.CurrentForce = Circle.CurrentForceInitial;
                }
            }
        }
        

    }
}