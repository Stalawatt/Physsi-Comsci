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


           
            
            public Vector2 CurrentForce;


            

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
                            thisnode.springList.Add(new Spring(thisnode, othernode , 0.3) );
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
                updateSpringForce();
                updateAcceleration();
                updateVelocity();
                updatePosition();
                
            }

            public void updateSpringForce()
            {
                foreach(Node node in CircleNodes)
                {
                    foreach (Spring spring in node.springList)
                    {
                        applyForce(spring.SpringForceFind(spring), node);
                    }
                }
            }
            public void unapplyForce(Vector2 Force, Node node)
            {
                node.CurrentForce = Vector2.Subtract(node.CurrentForce, Force);
            }

            public void applyForce(Vector2 Force, Node node)
            {
                node.CurrentForce = Vector2.Add(node.CurrentForce , Force);
            }

            private void updateAcceleration()
            {
                // A = F/M 

                foreach (Node node in CircleNodes)
                {
                    if (node.CurrentForce.Y < Options_Settings.Gravity.Constant )
                    {
                        node.CurrentForce.Y = Options_Settings.Gravity.Constant;
                    }
                    node.Acceleration = Vector2.Divide(node.CurrentForce, node.Mass);
                }

            }

            private void updateVelocity ()
            {
                foreach(Node node in CircleNodes)
                {

                    node.Velocity = Vector2.Add(node.Velocity, node.Acceleration);
                }
            }


            private void updatePosition()
            {
                foreach( Node node in CircleNodes)
                {
                    Vector2 NextPos = Vector2.Add(node.Position, node.Velocity);
                    

                    //throw new Exception(node.Acceleration.ToString());
                    // CHECK FOR COLLISIONS

                    // Use spacial partitioning if possible. otherwise just check against eachother

                    if (SB_COLLIDE.NodeCollisions.BoundaryCollision(node, NextPos) == true)
                    {
                        // do nothing as the boundarycollision function takes care of boundaries

                    } else
                    {
                        node.Position = NextPos;

                    }

                   

                }
            }



        }


        public class Node
        {

            // Circular nodes to show the particles that interact with the world

            public Vector2 Position;
            public Vector2 InitialPosition;
            public int ID;

            public List<Spring> springList = new List<Spring>();

            public Vector2 Acceleration = new Vector2(0,0);
            public Vector2 Velocity = new Vector2(0,0);

            public float Mass;

            public Vector2 CurrentForce;
            public Vector2 ForceConstant;

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

                ForceConstant = new Vector2(0, Options_Settings.Gravity.Constant );

            }

            
         
        }

        public class Spring
        {

            public Node Node1;
            public Node Node2;

            public double SpringConst;

            public double InitialLength;
            public double CurrentLength;

            public Vector2 SpringForce;

            public Spring(Node node1, Node node2, double springConstant)
            {
                Node1 = node1;
                Node2 = node2;
                SpringConst = springConstant;

                InitialLength = calcDistOfNodes(node1,node2);
                CurrentLength = InitialLength;
            }

            public double calcDistOfNodes(Node node1 , Node node2)
            {
                Vector2 PosA = node1.Position;
                Vector2 PosB = node2.Position;

                double DeltaX = Math.Pow(Math.Abs(PosA.X - PosB.X), 2);
                double DeltaY = Math.Pow(Math.Abs(PosA.Y - PosB.Y), 2);


                double distance = Math.Sqrt(DeltaX + DeltaY);

                return distance;
            }


            public Vector2 SpringForceFind (Spring spring)
            {
                Node node1 = spring.Node1;
                Node node2 = spring.Node2;

                return calcForceOfSpring(node1, node2);
            }


            public Vector2 calcForceOfSpring(Node node1, Node node2)
            {

                // F = k DeltaL


                Vector2 MidPoint = (node1.Position + node2.Position)/2;


                double currentDist = calcDistOfNodes(node1, node2);
                if (SpringConst == 0 || InitialLength == currentDist)
                {
                    return Vector2.Zero;
                } 

                
                
                double multiplier = -1 * (currentDist - InitialLength);

                Vector2 midToNode = MidPoint - node1.Position;



                Vector2 ForceAppliedNode1 = new Vector2((float)(midToNode.X * multiplier),(float)(midToNode.Y * multiplier ));





                return ForceAppliedNode1;
            }


            

        }


        
        public static void createCircleObj(ContentManager Content)
        {
            Circle newCircle = new Circle(200, 10, new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 , GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2));
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
                    node.CurrentForce = Vector2.Zero;
                }
            }
        }
        

    }
}