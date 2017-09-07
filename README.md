# JohnnyD_Standard_Asset
This asset is an unity base project with a premade menu and event pipeline to help produce games faster.


--------------
###Added Version 1.01:
You can dowload the package here: https://www.dropbox.com/s/qf2nsv1morx9rcg/JohnnyDalvi%20Standard%20Asset.1.01.unitypackage?dl=0

Changes:
* Win/Lose Panel;
* StartGame Event and isPlaying Master Bool;
* Fixed some small bugs.
--------------

OLD-This is the version 1.00.
You can dowload the package here: https://www.dropbox.com/s/xsk19nx8fzihqig/JohnnyDalvi%20Standard%20Asset.unitypackage?dl=0

--------------
Follow my work at my website:
# http://firepitgames.net



## This Asset Consists off:
* Singleton Master GameObject that is responsible for managing overall options such as Player Preferences, Custom Events, in-game message windows, pause/unpause, overall sound manager, musics and PlayerFeedback class;
* Custom UIEditor (Window/UI Editor) that can handle mass UI Image/Text/Outline color change;
* Premade Menu flow, with a loading scene, start menu and the Game scene with inbuilt options.

![alt text](http://firepitgames.net/wp-content/uploads/2017/08/giphy-1.gif)

This is the first version and I will be slowly updating it, everyone is free to send me pull-requests in order to help me improve it.

# Documentation

## Master GameObject:
The Master GameObject is a singleton, it starts by default in the first scene and will perpetuate during the execution. This GameObject handles the major functionalities of this asset using these following components:

### Master.Master.cs(Script)
This script handles the Custom events, those events can be triggered by using the Static Call: Master.(EventTrigger)() and you can assign methods to them using: Master.(EventName) += (your void MethodName). All methods that you asign to them will be called when the event happens. Please also note that this class can instantiate itself if you call the level manager and there is no MasterClass within the scene, it will load itself from the Resources folder, so I'd advise not to change that folder or anything within already in.

The custom events are:

* OnSceneLoaded: This event is just a callback to the regular unity SceneManager.sceneLoaded call, it will be called whenever you load a new scene;
* OnPause: This event is called when you pause the game using the inbuilt pause button or acessing the Master.PauseGame() method, it will change the timescale to 0;
* UnPauseGame: This event is called when you unpause the game using the inbuilt pause button or acessing the Master.UnPauseGame() method, it will reset the timescale to 1;
* OnReset: This event is still empty, but you can use it to make an reset system to a level, can be called using Master.ResetGame();
* OnWin: This event calls the WinSound within the AudioController, you can assign to it anything that you want to happen when the player wins, you can call it using Master.WinGame();
* OnLose: This event calls the LoseSound within the AudioController, you can assign to it anything that you want to happen when the player loses, you can call it using Master.LoseGame();

There is also a "Print Events" bool in this class, if you set it to true it will print in the console whenever an event gets called and the time that it happened.

### Master.LevelManager.cs(script):
This script is how we change scenes in this asset, there are a variety of public methods to change the scene based on scene index, name, the option to change to the next scene, and you can also reset the scene and Quit the application.

There is also the "LoadLevelAsync(int scene, Slider slider)" method, that you can load the scene asynchronously and even call it using an slider as parameter so it can show a loading bar progress.

There is an static instance access to this method, so you can call any public method by using LevelManager.instance.MethodName.

### Master.AudioController.cs(script):
This script holds the references to the Button Sound, Win Sound and Lose sound, whenver clip you put on those public clips will be played on the respective events.

This Script communicates with the player preferences and with the MenuMethods to change the Audio and Music Volumes too.

### Master.MusicManager.cs(script):
This script holds music clips within an array and play them accordingly to the active scene, if there are more scenes than musics, it will repeat the last clip.

### Master.PlayerPreferences.cs(script):
This script is responsible for saving the player preferences, in this current version it just saves the Audio preferences.

### Master.GUIOverallOptions.cs(script):
This script changes all the fonts in the scene (the ones that start active) to the font within the inspector, I will probably terminate this class once I implement this option in the UI Editor custom window.

## Master Canvas Classes:
I've added a canvas to the Master GameObject, this canvas should be used to things that aren't related to the actual Menu UI, in this version it have two classes:

### Master.MessageLogger.cs(script):
This class is used to show message panels in the game, can be used in ingame conversations or system messages to the player. The methods within this class are static, so you can call it just by using MessageLogger.MethodName(args).

Methods:

* DisplaySystemMessage(string message): This method opens the System Message panel and pause the game, once the player press the continue button, the game will be unpaused. It will be shown the string within the parameter to the player.

* DisplayChatText(string message, Sprite image, bool pause) and DisplayChatText(string message, bool pause): This method opens the Chat Panel, you can either choose to pause the game or not, and you can use an sprite as parameter too to show some chararcter plate or icons.

### Master.PlayerFeedback.cs(script):
This class is used to show player feedback graphics in the Master Canvas using ingame transform.position references (scrolling text for example), the methods within it are statics too, so you can call it straight away using PlayerFeedback.MethodName(args). Please note that this class uses prefabs within the Resource folder, so I'd advise not to change that folder or anything within already in.

Methods:

* public static Text TextFromWorldToCanvas(string text, Color color, Vector3 worldPosition, float timerToDestroy, Vector2 CanvasMoveVelocity):  This method will instantiate an text in the Master Canvas using the prefab within the resource folder, The text will say whatever you put in the string parameter and it will be instantiated using a world position as reference and will be destroyed accordingly to the timer. There are a few overload options to this method, you can also add movement over time to it so it can actually scroll accordingly to a Vector2 and you can also change the text color depending on the overload. This method can return a Text, so you can call it and still reference the text within the script that is calling it, this can be useful if you want to change something about the text afterwards.

* public static Image ImageFromWorldToCanvas(Sprite image, Vector2 rectSize, Vector3 worldPosition, float timerToDestroy, Vector2 CanvasMoveVelocity): Similar to the TextFromWorldToCanvas method, although it wil accept an image as input. It can also returns an image, so you can change it afterwards calling it.

## MenuMethods.cs(script):
This script is attached to the main Menu Canvas of each scene and comunicates with the Master GameObject in order to handle the menu methods. It does things such as open the options panel, changing level, changing preferences, pausing the game etc.
It also have an AudioSource that is used to play the Button Sound and Win/Lose sounds.

Social Media and website: the MenuMethods.cs have a method that accepts an string as input, it will open any URL that you put in the string parameter.

## Custom UI Editor(window):
This is an Editor script, so it should be accessed during edit mode and not play mode, you can open the respective window in > Window/UI Editor. I shall be improving this window soon, I want to add font and image support.

### How it works:
This class is used to mass change Images,Buttons and texts that are currently ACTIVE in the current scene, you can address the colors that you want it to use and when you press "Change Colors" it will change all the UI. There are 4 default scheme options and you can also add custom tags, it will save anything that you put there, just beware that if you reset player preferences it will reset these options too.

### Default Schemes:
Buttons: It will change the color of all buttons within the scene, it will also change the Outline if there is any within that button;
Button's text: It will change the color and outline of the text within the buttons in the scene, if there are any.
Other Text: It will change the color and outline of the text that aren't within a button;

### Custom Schemes(tag):
The custom schemes works based on the tags, please note that the Custom Scheme have priority over the default scheme, anythin you add here will subscribe the button/button's text/other text configuration. You can add as many custom schemes as you want, you can also delete them.

### Non Effect (tag):
If you want this system to don't affect an specific object or text, you can just assign the specific tag that you have chosen to put here to the object.


# Thanks and Licensing:

You can use it for anything that you want, just follow the licensing in the "<a href="https://github.com/JohnnyDalvi/JohnnyD_Standard_Asset/blob/master/License.txt">License.txt</a>" within the root folder. It would also be good to have contribution to this asset, I think that it is pretty useful to speed production, mainly for game jams.

You can e-mail me at:
## johnnydalvi.games@gmail.com


### Thanks!
