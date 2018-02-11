A complete rewrite in Unity3D of the multi-agent systems game Warbot.
Scripted bot war game in Unity 3D

[![Alt text](https://78.media.tumblr.com/tumblr_ovr8ilVtCk1wv3k0d_smart1.jpg)](https://vt.media.tumblr.com/tumblr_ovr8ilVtCk1wv3k0d_480.mp4)

# A short history of Warbot  
The Warbot project starts in 2001 with Jacques Ferber, teacher and researcher at Montpellier University, France. The aim is to make a simple wargame/RTS controlled via player-written scripts. There are two main selling points for this idea. First, it's fun. Second, it's a great way to teach programming, and more specifically agents oriented programming, to students.  
Now, the oldest version of Warbot was developped as a part of [MadKit](http://www.madkit.net/madkit/), a Java library and integrated environment for the developpment of Multi-Agent systems.   
![warbot1](http://www.madkit.net/warbot/images/warbot_action.png "Warbot 1, pretty ugly right?")  
Then came Warbot 2, also known as [Warlogo](http://www.lirmm.fr/~ferber/ProgAgent/warlogo-doc.pdf) which was, as its name suggests, written in [NetLogo](https://ccl.northwestern.edu/netlogo/), a simple language for writing Multi-Agent Systems (unfortunately I couldn't find a screencap for that one but I expect it was pretty ugly).
Finally, the last version of Warbot, and then one still in use until this year is Warbot 3. We're back to Java for this one, but it's a standalone program that doesn't require MadKit to run. ![It's still pretty barebones in terms of visuals](http://www.lirmm.fr/~ferber/ProgAgent/images/interface-warbot3.png "It's still pretty barebones in terms of visuals"), but it integrates all kinds of new features (debug, game statistics and so on) making it a great teaching tool. Its last issue? It's not so great as a game. Just look at it. 
And this is why and how Warbot 4 came to be. Well, not quite yet.  

# Warbot 4
I've been lying. The developpment of Warbot 4 doesn't start with this entry. It's actually been a few months in the making, by myself and a group of students from university, as part of an assignment. Here were our requirements:  

>**Make Warbot in Unity, make it pretty, and make it easier to play.** 

And that we did. Kind of. We only had 3 months and a half to learn Unity (and C# for some of us), and rewrite the whole thing from scratch. While at the same time dealing with other courses and exams. Needless to say compromises were made, but in the end, I think what we came up with wasn't so bad. The main and most visible difference with the previous versions (apart from the 3D graphics of course), is the scripting editor being implemented inside the game, and letting players write scripts in a visual language of our own, very similar to Scratch. Previous versions required you to write your scripts in Java or Python, which admittedly gives you more freedom, but also makes it more difficult for beginners (which is the target audience) and makes it harder to avoid cheating.  
The behavior here is driven by a simple subsumption architecture, which lets you define for each unit type a list of tasks, that will be executed in a priority order set by the player depending on the unit's internal and external percepts (enemy units around it, health level etc.).  
You can find the result [here](https://github.com/ecrvnr/warbot4-prototype) and play around with it if you can figure out the behavior editor. I'm not gonna detail every piece of it here because, I will end up doing that later on as this project progresses, and because you can find a fairly detailed paper on this prototype [right here](https://github.com/ecrvnr/warbot4-prototype/blob/master/warbot_unity.pdf).  
At least it wasn't so bad if we only cared about getting a good grade and moving on. That's not the case for me. I look at it and I can't stop thinking about all the ways it could be better.  
Which is exactly what drives this project. I'm gonna work on building a *real* Warbot 4, and try to fix what went wrong with our prototype. In no particular order and at the top of my head:  

+ Improve the graphics. *A lot*. Not included in this: the unit models which are mostly pretty good. This will involve dropping the terrible map and decor we have now and probably moving to a more cartoony and block-based thing, which my make movements in general easier.  
+ Improve performance. **_A LOT_**. Seriously, it runs horribly as soon as you go over 50-ish units on the map at once. Considering the complexity of the models and the scripts driving them, this probably shouldn't happen.
+ Improve the subsumption system. One of the biggest drawbacks of it so far, and one of the thing that disappointed me the most while working on the prototype was that we can't allow for any of the actions to happen over more than one frame, because of the way the subsumption interpreter was implemented. Which means that, for example, we had to make rotations happen over one frame, and our system doesn't allow players to set up tasks that happen over a certain amount of time (walk to x position, then rotate, then shoot, for example). This is also probably the reason for the game's poor performance, as each unit has to go through their behaviour tree every frame. This has to change.  
+ Speaking of subsumption, the behaviour editor is kind of ugly and not so easy to use. It was written in OpenGL straight up because we couldn't quite figure out how to move the action labels around in a 2D space in Unity3D, and we were somewhat pressed by time.  


And so here we are, starting over, again. Hopefully, everything will go right. I'm hoping to have a working base by the end of the summer (general visual identity, simple game scene with some behaviors working, maybe a preliminary version of the behavior editor). That's all for now, I'll make sure to blog the heck out of every little thing I do. See ya.
