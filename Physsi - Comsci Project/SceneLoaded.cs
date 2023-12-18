
using System;
using System.Security.Cryptography.X509Certificates;

namespace Physsi___Comsci_Project
{
    public class SceneLoaded 
    {
        private static string[] scene_List = new string[5]; // scenes possible to be loaded



        public static string scene_Loaded = "HOME"; // currently loaded scene

       

        public void scene_load() // load scenes into the scene_List array
        {
            scene_List[0] = "HOME";
            scene_List[1] = "OPTIONS";
            scene_List[2] = "RIGIDBODY_EDITOR";
            scene_List[3] = "SCENEPLAYER";
            return;
        }


        public void check_errors() // check for errors in loading
        {
            if (scene_Loaded == null)
            {
               throw new Exception("scene_Loaded is null"); // If 'scene_Loaded' variable is null, throw error
            }


            if (Array.IndexOf(scene_List,scene_Loaded) == -1) // the value of 'scene_Loaded' is not in the list (and so not an accepted scene)
            {
                throw new Exception("scene_Loaded is not in scene_List"); //  throws error to show what is wrong 
            }


        }
        



    }
}
