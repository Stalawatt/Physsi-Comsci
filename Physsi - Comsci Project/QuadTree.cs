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
    public class QuadTree 
    {

        
        private int maxPoints;

        private Vector2 boundaryX; // Lower Boundary , Higher Boundary

        private Vector2 boundaryY; // Lower Boundary , Higher Boundary

        private RB_LOGIC.Square_Item[] Items; // list of all the squares in the scene 

        private List<QuadTree> Children = new List<QuadTree>(); // List of all direct child nodes (exactly 4)

        public QuadTree(int maxPoints, Vector2 boundaryX, Vector2 boundaryY, RB_LOGIC.Square_Item[] Items, QuadTree Parent)
        {
            this.maxPoints = maxPoints;
            this.boundaryX = boundaryX;
            this.boundaryY = boundaryY;
            this.Items = Items;

            if (Items != null && countItems() > maxPoints) 
            {
                createChildren();
            }

            
        }

        private int countItems()
        {
            return Items.Length;
        }

        private RB_LOGIC.Square_Item[] findc1Items(float midx, float midy)
        {

            RB_LOGIC.Square_Item[]in_Square = new RB_LOGIC.Square_Item[Items.Length];
            int counter = -1;

            foreach (RB_LOGIC.Square_Item Square in Items)
            {
                if (Square.Position.X > boundaryX.X && Square.Position.X < midx)
                {
                    if (Square.Position.Y > boundaryY.X &&  Square.Position.Y < midy)
                    {
                        in_Square[counter++] = Square;

                    }
                }
            }

            return in_Square;
        }
        private RB_LOGIC.Square_Item[] findc2Items(float midx, float midy)
        {

            RB_LOGIC.Square_Item[] in_Square = new RB_LOGIC.Square_Item[Items.Length];
            int counter = -1;

            foreach (RB_LOGIC.Square_Item Square in Items)
            {
                if (Square.Position.X < boundaryX.Y && Square.Position.X > midx)
                {
                    if (Square.Position.Y > boundaryY.X && Square.Position.Y < midy)
                    {
                        in_Square[counter++] = Square;

                    }
                }
            }

            return in_Square;
        }

        private RB_LOGIC.Square_Item[] findc3Items(float midx, float midy)
        {

            RB_LOGIC.Square_Item[] in_Square = new RB_LOGIC.Square_Item[Items.Length];
            int counter = -1;

            foreach (RB_LOGIC.Square_Item Square in Items)
            {
                if (Square.Position.X > boundaryX.X && Square.Position.X < midx)
                {
                    if (Square.Position.Y < boundaryY.X && Square.Position.Y > midy)
                    {
                        in_Square[counter++] = Square;

                    }
                }
            }

            return in_Square;
        }

        private RB_LOGIC.Square_Item[] findc4Items(float midx, float midy)
        {

            RB_LOGIC.Square_Item[] in_Square = new RB_LOGIC.Square_Item[Items.Length];
            int counter = -1;

            foreach (RB_LOGIC.Square_Item Square in Items)
            {
                if (Square.Position.X < boundaryX.X && Square.Position.X > midx)
                {
                    if (Square.Position.Y < boundaryY.X && Square.Position.Y > midy)
                    {
                        in_Square[counter++] = Square;

                    }
                }
            }

            return in_Square;
        }

        private void createChildren()
        {
            float middleboundaryX = (boundaryX.X + boundaryX.X) / 2;
            float middleboundaryY = (boundaryY.X + boundaryY.Y) / 2;

            

            QuadTree Child1 = new QuadTree(maxPoints, new Vector2(boundaryX.X, middleboundaryX), new Vector2(boundaryY.X, middleboundaryY), findc1Items(middleboundaryX, middleboundaryY), this); // top left
            QuadTree Child2 = new QuadTree(maxPoints, new Vector2(middleboundaryX, boundaryX.Y), new Vector2(boundaryY.X, middleboundaryY), findc2Items(middleboundaryX,middleboundaryY), this); // top right
            QuadTree Child3 = new QuadTree(maxPoints, new Vector2(boundaryX.X, middleboundaryX), new Vector2(middleboundaryY, boundaryY.Y), findc3Items(middleboundaryX, middleboundaryY), this); // bottom left
            QuadTree Child4 = new QuadTree(maxPoints, new Vector2(middleboundaryX, boundaryX.Y), new Vector2(middleboundaryY, boundaryY.Y), findc4Items(middleboundaryX, middleboundaryY), this); // bottom right

            Children.Add(Child1);
            Children.Add(Child2);
            Children.Add(Child3);
            Children.Add(Child4);

        }


        public void checkCollisions()
        {
            if (Children.Count == 0)
            {
                foreach(RB_LOGIC.Square_Item Square in Items)
                {
                    foreach (RB_LOGIC.Square_Item Square2 in Items)
                    {
                        if (Square != Square2)
                        {
                            if ()
                            {

                            }
                        }
                    }
                }
            }
        }



    }
}
