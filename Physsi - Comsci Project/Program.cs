using Microsoft.Xna.Framework;
using System.Linq;

var scene = new Physsi___Comsci_Project.SceneLoaded();
using var game = new Physsi___Comsci_Project.Main();
scene.scene_list_load();
scene.check_errors();
game.Run();













