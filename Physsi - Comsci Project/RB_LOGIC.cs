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
using System.Security.Cryptography.X509Certificates;

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

       
    
        private class Items
        {
            public List<Square_Item> Square_Items = new List<Square_Item>();

        }

        

        private class Square_Item
        {
            
            // storing information about the object
            public Vector2 Position; 
            public Texture2D Sprite;

            public Vector2 Position_Scaled; // position inside of the smaller screen for the RB editor

            public Vector2 Velocity; // stores the component velocities for horizontal and vertical
            public Vector2 Acceleration; // stores component acceleration for horizontal and vertical

            public Vector2 Center;

            public Vector2 Find_Center(Vector2 position) //  finds the center of the square 
            {
                Vector2 to_center = new Vector2(Sprite.Width/2,Sprite.Height/2);
                return Vector2.Add(position,to_center);
            }


            public void Update() // update attributes for the object
            {

                Update_Velocity();
                Update_Position();
                
            }
            
            private void Update_Velocity() // V = u + a * t
            { // Determines velocity for next frame
                
                Vector2 deltaVelocity = new Vector2(Acceleration.X * deltaTime.GetDeltaTime(), Acceleration.Y * deltaTime.GetDeltaTime());
                Velocity = Vector2.Add(Velocity, deltaVelocity);
                
                
            }

            private void Update_Position() // updates the position by S_nextFrame = S_currentFrame + V * t
            { // determines position for next frame

                Vector2 deltaPosition = new Vector2(Velocity.X * deltaTime.GetDeltaTime(), Velocity.Y * deltaTime.GetDeltaTime());
                Position = Vector2.Add(Position, deltaPosition);
                Position_Scaled = Vector2.Multiply(Position, screen_ratio_constant);
            }
            

        }

        private static Items itemList = new Items();

        private static Square_Item GenerateDefaultSquare(ContentManager Content) // creates a default square item
        {
            Square_Item Square = new Square_Item(); // instantiates a new object

            Square.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Square_Item"); // sets the sprite

            Square.Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Square.Sprite.Width)/2,
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Square.Sprite.Height)/2); // Centers the sprite in the middle of the screen

            Square.Position_Scaled = Vector2.Add(Vector2.Multiply(Square.Position, screen_ratio_constant), TopLeft_RB_Preview); // scales the position for the RB preview scene

            Square.Velocity = new Vector2(0,0); // velocity of square
       
            Square.Acceleration = new Vector2(0, Options_Settings.Gravity.Constant); // sets default acceleration to be X:0, Y:Gravitational Constant stored in Options_Settings

            Square.Center = Square.Find_Center(Square.Position); // finds the center of the square
            
            return Square;
        }
        public static void Add_Square_Item(ContentManager Content) // adds a new instance of the Square_Item object to the list in Items class
        {
            Square_Item Default = GenerateDefaultSquare(Content);
            itemList.Square_Items.Add(Default);
        }
        
        public static void Draw_RB_PREVIEW(SpriteBatch _spriteBatch) // draws the squares to the preview in the rigidbody editor
        {
            deltaTime.Start();
            
            foreach (Square_Item Square in itemList.Square_Items)
            { 
                _spriteBatch.Draw(Square.Sprite, Square.Position_Scaled, Color.White);
                Square.Update();
                
            }

            deltaTime.End();
            
        }





    }
}
