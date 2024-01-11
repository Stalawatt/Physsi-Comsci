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
    public class RB_COLLIDE
    {
        // Logic for collisions in the rigidbody player
        public static RB_LOGIC.Square_Item CollidedWith;
        
        public static bool checkSquare(Vector2 Position, Vector2 deltaPosition, RB_LOGIC.Square_Item self)
        {
            foreach (RB_LOGIC.Square_Item Square in RB_LOGIC.itemList.Square_Items){

                Vector2 nextFramePos = Vector2.Add(Position, deltaPosition); // finds position for next frame

                if (nextFramePos.X >= Square.Position.X && nextFramePos.X <= Square.Position.X + 150 && nextFramePos.Y + 100 >= Square.Position.Y && self != Square)
                {
                    CollidedWith = Square;
                    return true;
                }

            }
            

            return false;
        }

    }
}
