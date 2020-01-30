# Laser-Defender
This is classical unity 2D game where there is a fight spaceships. 
Player trys to kill the enemy spaceships with lase bullets.

Commit-1: Created The Repo - This is the start point for The Project

commit-2: Setted up The Project - Created the background, set up the canvas,
and added player and enemy sprite in the scene

commit-3: Player Moving Script Added - Added Player Script in the player Object so it can move
on press of up, down, left and right key.

Commit-4: Moving Boundary Added For The Player - Boundary Added for the player space ship,
so it can not go out of the of the screen(Out of Boundary).

Commit-5: Player Shooting mechanism added - Now Player Space ship can shoot laser 
when the space bar is pressed. one press of the space bars shoots one laser 

Commit-6: Continuos Shotting enabled - Now player can shoot continuosly while keep pressing the
space buton down, and after relasing the space key the shotting will stop

Commit-7: Bullets Destroyed After They Exit Play Area - The Laser Bullets were making a mess in
the hiarchy amd they were present the world for infinite time. Now they will be destroyed after
collidng with the Shredder.

Commit-8: Enemy Moving Mechanism added - Now The Single Enemy present in the game
Can move following a path.

Commit-9: Enemy moving in various path Mechanism added - Now There are two waves(two wave objects)
now the enemy can move in two wave either in the first wave or in the second wave.
depends on which wave has been assigned to it

Commit-10: Multiple Enemies Spawning in the same wave once - Multiple Emenies can create themselves
in a particular wave, they follow a path and destroy themselves. Only one wave can be done by the enemies

Commit-11: Enemy Can be assigned any Path - Now the enemy can follow any path assigned from the Enemy
Spawner Script still there is one spawn of the enemies.

Commit-12: Multiple Enemies coming from multiple wavws - There are two enemies any and two 
waves now. Each enemy follow a specific wave.

Commit-13: Enemies Coming for infinite time - Now there are 4 waves of enemy
comming for infinte time by repaeting the loop

Commit-14: Many New Waves Created- New Paths and new enemies have been added in many new waves

Commit-15: More Waves added - There are 32 different waves with 20 paths and moro enenmy prefabs

Commit-16: Health added to roger and bernerred - Roger abd bernerred are two types of enemies
they have health and the health dicrease whrn bulltes hit the collider of the two enemies.

Commit-17: Enemy Destroy Mechanism added - Now The enemy is destroyed when the health of the enemy
is 0 or less, it only works with two enemies Roger and Bernered

Commit-18: Enemy Shotting Mechansim added - Now the Enemy can shoot , Only Roger and bernerred

Commit-19: Shotting and damage mechanism added to all enemy - All the enemies can now shoot,
the have a health and when health is 0 then they destroy.

Commit-19: Player Hit and destroy mechanism added - Now The player have a health and it recieves
the bullet from enemy. When health is below 0 it destroyes itself

Commit-20: Player and enemy destroying themselves problem solved - Previously player was killed
by its own bullet and was also killing itself by his own bullets. Now layers have been added
so that the player bullet only hits the enemy and enemy bullets only hit the player

Commit-21: Scrolling Background added - Now the game scene has a scrolling background so i feels like


