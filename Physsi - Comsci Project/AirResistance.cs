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
    public class AirResistance
    {
        public static float CalculateAirResistanceSquareRB(RB_LOGIC.Square_Item Square)
        {

            if (Options_Settings.AirResistance.state == false)
            {
                return 0f;
            }

            float airResistance = 0f;

            float Velocity = Square.Velocity.Y;

            float ExposedSA = Square.Sprite.Width;

            airResistance = (Options_Settings.AirResistance.dragCoefficientSquare * Options_Settings.AirResistance.airDensity * (Velocity * Velocity) * ExposedSA) / 2;

            return airResistance;
        }


        public static Vector2 CalculateAirResistanceCircleSB(SB_LOGIC.Node node)
        {

            if (Options_Settings.AirResistance.state == false)
            {
                return Vector2.Zero;
            }

            Vector2 airResistance;

            Vector2 Velocity = node.Velocity;

            float ExposedSA = 0.5f * (float)(Math.PI) * 1.25f;

            airResistance = (Options_Settings.AirResistance.dragCoefficientSquare * Options_Settings.AirResistance.airDensity * (Velocity * Velocity) * ExposedSA) / 10;

            return airResistance;
        }
    }
}
