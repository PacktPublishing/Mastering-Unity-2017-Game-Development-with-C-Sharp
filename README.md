# Mastering Unity 2017 Game Development with C# - Second Edition
This is the code repository for [Mastering Unity 2017 Game Development with C# - Second Edition](https://www.packtpub.com/web-development/mastering-unity-2017-game-development-c-second-edition?utm_source=github&utm_medium=repository&utm_campaign=9781788479837), published by [Packt](https://www.packtpub.com/?utm_source=github). It contains all the supporting project files necessary to work through the book from start to finish.
## About the Book
Do you want to make the leap from being an everyday Unity developer to being a pro game developer? Then look no further! This book is your one-stop solution to creating mesmerizing games with lifelike features and amazing gameplay.

This book focuses in some detail on a practical project with Unity, building a first-person game with many features. You'll delve into the architecture of a Unity game, creating expansive worlds, interesting render effects, and other features to make your games special. You will create individual game components, use efficient animation techniques, and implement collision and physics effectively. Specifically, we'll explore optimal techniques for importing game assets, such as meshes and textures; tips and tricks for effective level design; how to animate and script NPCs; how to configure and deploy to mobile devices; how to prepare for VR development; how to work with version control; and more.

By the end of this book, you'll have developed sufficient competency in Unity development to produce fun games with confidence.

## Instructions and Navigation
All of the code is organized into folders. Each folder starts with a number followed by the application name. For example, Chapter02.



The code will look like the following:
```
public IEnumerator StateIdle() 
    { 
        //Run idle animation 
        ThisAnimator.SetInteger("AnimState", (int) ActiveState); 
        //While in idle state 
        while(ActiveState == AISTATE.IDLE) 
        { 
            yield return null; 
        } 
    } 
```

To read this book effectively, and to complete the tasks within, you need only two things: first, the Unity 2017 software (which you can get for free from https://unity3d.com) and second, the determination to succeed! By using only these tools, you can learn to produce great games in Unity.

## Related Products
* [Mastering Unity 5.x](https://www.packtpub.com/game-development/mastering-unity-5x?utm_source=github&utm_medium=repository&utm_campaign=9781785880742)

* [Unity 5.x By Example](https://www.packtpub.com/game-development/unity-5x-example?utm_source=github&utm_medium=repository&utm_campaign=9781785888380)

* [Mastering Unity Scripting](https://www.packtpub.com/game-development/mastering-unity-5x-scripting?utm_source=github&utm_medium=repository&utm_campaign=9781784390655)

