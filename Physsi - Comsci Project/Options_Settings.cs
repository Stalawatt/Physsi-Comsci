﻿using Microsoft.Xna.Framework;
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
    public class Options_Settings

    {
        public static class AirResistance
        {
            public static bool state = false; // on or off (starts on off)
            public static float airDensity = 0.0005f;
            public static float dragCoefficientSquare = 1.05f;
            public static float dragCoefficientCircle = 0.047f;
        }

        public static class Gravity
        {
            public static float Constant = 0.1f; 
        }
       
       
    }
}
