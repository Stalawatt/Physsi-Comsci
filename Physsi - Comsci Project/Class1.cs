
using System;

namespace Physsi___Comsci_Project
{
    public class SceneLoaded 
    {
        private string[] scene_List = new string[5];



        private string scene_Loaded = "HOMESCREEN";

        public void scene_list_load()
        {
            scene_List[0] = "HOMESCREEN";
            scene_List[1] = "OPTIONS";

            return;
        }

        public void replace_loaded_scene(string scene)
        {
            scene_Loaded = scene;
            return;
        }

        public void check_errors()
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
