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
using System.Threading.Tasks;

namespace Physsi___Comsci_Project
{
    public class SB_COLLIDE
    {
        public class NodeCollisions
        {
            public static bool BoundaryCollision(SB_LOGIC.Node Node, Vector2 NextPos)
            {
                if (NextPos.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Node.Radius * 2 )
                {
                    

                    return true;
                }
                return false;
            } 
        }




    }
}
