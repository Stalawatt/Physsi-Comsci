using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physsi___Comsci_Project
{
    public class ChangeScene
    {
        
        public static void changeTo(string scene)
        {
            SceneLoaded.scene_Loaded = scene;
            
            return;
        }
    }
}
