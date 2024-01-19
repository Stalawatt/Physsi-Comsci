using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Physsi___Comsci_Project
{
    public class RB_COLLIDE
    {

        // using spacial partitioning technique - splits screen into boxes and treat each individual box as a screen so that only items in each box can collide with eachother
        //                                        so less checks to be done so is faster


        public static RB_LOGIC.Square_Item CollidedWith;

        public static bool checkSquare(Vector2 Position, Vector2 deltaPosition, RB_LOGIC.Square_Item self)
        {
            foreach (RB_LOGIC.Square_Item Square in RB_LOGIC.itemList.Square_Items)
            {

                Vector2 nextFramePos = Vector2.Add(Position, deltaPosition); // finds position for next frame

                if (nextFramePos.X <= Square.Position.X + 150 && nextFramePos.X + 150 >= Square.Position.X && (nextFramePos.Y + 150 >= Square.Position.Y && nextFramePos.Y <= Square.Position.Y + 150) && self != Square)
                {
                    CollidedWith = Square;
                    self.Acceleration = new Vector2(0, 0);
                    self.Velocity = new Vector2(0, 0);
                    return true;
                }

            }


            return false;
        }



    }
}
