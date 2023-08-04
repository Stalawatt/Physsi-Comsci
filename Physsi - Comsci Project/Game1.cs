using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Physsi___Comsci_Project
{
    public class HomeScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // HOMESCREEN SPRITES LOADING TEXTURE2D
        private Texture2D Rigid_Button;
        private Texture2D Options_Button;

        // SPRITEFONTS FOR TEXT
        private SpriteFont PhyssiLogo;

        // TEXT 
        private string PhyssiText = "Physsi"; // TITLE
          

        // HOMESCREEN BUTTON POSITIONS
        Vector2 Rigid_Body_Pos = new Vector2(460, 400); // From top left of screen
        Vector2 Soft_Body_Pos = new Vector2(460, 600); // From top left of screen
        Vector2 Options_Button_Pos = new Vector2(585, 800); // From top left of screen


        static class Physsi_Text_Width
        {
            public static float width;
            public static Vector2 Physsi_Pos = new Vector2( 680, 50); // From top left of screen
        }

        public HomeScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            

            // CHANGE TO FULLSCREEN

            _graphics.ToggleFullScreen();
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // INITIALISE TEXTURES
            Rigid_Button = Content.Load<Texture2D>("1000x174 Rigidbody Button");
            Options_Button = Content.Load<Texture2D>("Options button");

            PhyssiLogo = Content.Load<SpriteFont>("Physsi");


            Physsi_Text_Width.width = PhyssiLogo.MeasureString("Physsi").X * 150;


            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            _spriteBatch.Begin();
            _spriteBatch.Draw(Rigid_Button, Rigid_Body_Pos, Color.White);
            _spriteBatch.Draw(Rigid_Button, Soft_Body_Pos, Color.White);
            _spriteBatch.Draw(Options_Button, Options_Button_Pos, Color.White);
            _spriteBatch.DrawString(PhyssiLogo, PhyssiText, Physsi_Text_Width.Physsi_Pos, Color.White);
    

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}