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
    public class RB_LOGIC
    {
        // this file will contain the logic for the rigidbody editor's scene that will be played, including : 
        // 
        // instantiated classes of all the objects, in a list, saved with their mass, acceleration, speed, position etc.
        // the time ran for 
        // the data that will eventually be exported into a file



        
        private static double previousTime = 0;

        private static float screen_ratio_constant = 0.8536f;
       

        private static int min_Y = 160; // the positions of the smaller screen on the rigidbody editor's sides as X and Y lines
        private static int max_Y = 920; // gives around the same dimentions as the 1920x1080 screen
        private static int min_X = 0;   // these are used to check if the object is within the screen
        private static int max_X = 1638;

        private static Vector2 TopLeft_RB_Preview = new Vector2(min_Y, min_X);

       

       
    
        public class Items
        {
            public List<Square_Item> Square_Items = new List<Square_Item>();

        }

        public static Items itemList = new Items();

        private static QuadTree QuadTree = new QuadTree(2, new Vector2(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), new Vector2(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), itemList.Square_Items);

        public static class Click
        {
            public static MouseState LastClick;
        }

        public class Square_Item
        {
            
            // storing information about the object
            public Vector2 Position; 
            public Texture2D Sprite;
            

            public Vector2 Start_Position;
            public Vector2 Position_Scaled; // position inside of the smaller screen for the RB editor

            public Vector2 Velocity; // stores the component velocities for horizontal and vertical
            public Vector2 Acceleration; // stores component acceleration for horizontal and vertical
            public float Mass = 1f; // all start with a mass of 1 (kg)

            public Vector2 Center;

            public Vector2 Force;


            public Vector2 Find_Center(Vector2 position) //  finds the center of the square 
            {
                Vector2 to_center = new Vector2(Sprite.Width/2,Sprite.Height/2);
                return Vector2.Add(position,to_center);
            }


            public void Update() // update attributes for the object
            {
                nextAcceleration();
                nextVelocity();
                nextPosition();
            }

            public void Update_Force(Vector2 Force2)
            {
                Force = Vector2.Add(Force,Force2);
            }
            
            public void nextAcceleration()
            {
                Acceleration = Vector2.Divide(Force,Mass); // as F = M * A and so therefore A = F / M
            }

            public void nextVelocity()
            {
                Velocity = Vector2.Add(Velocity,Acceleration);
            }

            public void nextPosition()
            {
                
                Vector2 nextPosition = Velocity * deltaTime.GetDeltaTime();
                if (RB_COLLIDE.checkSquare(Position, nextPosition, this))
                {
                    Position = new Vector2(Position.X, RB_COLLIDE.CollidedWith.Position.Y - 150);
                } else
                {
                    if (Vector2.Add(Position, nextPosition).Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 150)
                    {
                        Position.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 150;
                    }
                    else
                    {
                        Position = Vector2.Add(Position, nextPosition);
                        
                    }
                }
            }

            

        }

        

        private static Square_Item GenerateDefaultSquare(ContentManager Content) // creates a default square item
        {
            Square_Item Square = new Square_Item(); // instantiates a new object

            Square.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Square_Item"); // sets the sprite

            Square.Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Square.Sprite.Width)/2,
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Square.Sprite.Height)/2); // Centers the sprite in the middle of the screen
            Square.Start_Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Square.Sprite.Width) / 2,
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Square.Sprite.Height) / 2); // saves where it starts

            Square.Position_Scaled = Vector2.Add(Vector2.Multiply(Square.Position, screen_ratio_constant), TopLeft_RB_Preview); // scales the position for the RB preview scene

            Square.Velocity = new Vector2(0,0); // velocity of square
       
            Square.Acceleration = new Vector2(0,0); // sets default acceleration to be X:0, Y:Gravitational Constant stored in Options_Settings

            Square.Center = Square.Find_Center(Square.Position); // finds the center of the square

            Square.Force = new Vector2 (0, Options_Settings.Gravity.Constant * Square.Mass); // initial force applied
            
            return Square;
        }
        public static void Add_Square_Item(ContentManager Content) // adds a new instance of the Square_Item object to the list in Items class
        {
            Square_Item Default = GenerateDefaultSquare(Content);
            itemList.Square_Items.Add(Default);
        }
        
        public static void Draw_RB_PREVIEW(SpriteBatch _spriteBatch) // draws the squares to the preview in the rigidbody editor
        {
           
            
            foreach (Square_Item Square in itemList.Square_Items)
            { 
                _spriteBatch.Draw(Square.Sprite, Square.Position_Scaled, Color.White);

            }

            
        }

        public static void Draw_RB(SpriteBatch _spriteBatch)
        {
            deltaTime.Start();
            foreach(Square_Item Square in itemList.Square_Items)
            {
                _spriteBatch.Draw(Square.Sprite, Square.Position, Color.White);
                Square.Update();
            }
            deltaTime.End();
        }

        public static void Reset_Squares()
        {
            foreach (Square_Item Square in itemList.Square_Items)
            {

                Square.Position = Square.Start_Position;
                Square.Velocity = new Vector2(0,0); // set velocity to 0
                
            }
        }

        public static void DragCLick(MouseState prevState) // drags the square items around the RB_EDITOR
        {
            
            MouseState currentState = Mouse.GetState();

            int deltaX = currentState.X - prevState.X;
            int deltaY = currentState.Y - prevState.Y;

            Vector2 deltaPos = new Vector2(deltaX, deltaY);

            foreach (Square_Item Square in itemList.Square_Items)
            {
                if (Click.LastClick.X >= Square.Position.X && Click.LastClick.X <= Square.Position.X + 150 && Click.LastClick.Y >= Square.Position.Y && Click.LastClick.Y <= Square.Position.Y + 150)
                {
                    Square.Position = Vector2.Add(Square.Position,deltaPos);
                    Square.Position_Scaled = Square.Position;
                    Square.Start_Position= Square.Position;
                }
            }                                                                                

        }

        public static void ResetScene()
        {
            itemList.Square_Items.Clear();
        }
                                                                                                              
    }
}
