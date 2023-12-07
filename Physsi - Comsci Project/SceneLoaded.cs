
using System;
using System.Security.Cryptography.X509Certificates;

namespace Physsi___Comsci_Project
{
    public class SceneLoaded 
    {
        private string[] scene_List = new string[5]; // scenes possible to be loaded



        public static string scene_Loaded = "Home"; // currently loaded scene

       

        public void scene_list_load() // load scenes into the scene_List array
        {
            scene_List[0] = "HOME";
            scene_List[1] = "OPTIONS";
            scene_List[2] = "RIGIDBODY_EDITOR";
            return;
        }

        public void replace_loaded_scene(string scene) // change scene currently loaded
        {
            scene_Loaded = scene;
            check_errors();
            return;
        }

        public void check_errors() // check for errors in loading
        {
            if (scene_Loaded == null)
            {
                System.Environment.Exit(1); // If 'scene_Loaded' variable is null, exit with code 1 (pointing to this error) 
            }


            if (Array.IndexOf(scene_List,scene_Loaded) == -1) // the value of 'scene_Loaded' is not in the list (and so not an accepted scene)
            {
                System.Environment.Exit(2); //  exits program and returns 
            }


        }
        



    }
}
