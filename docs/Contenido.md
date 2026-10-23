## Arte

-   Main menu
	-   Logo
	-   Boton Play 
	
-   HUD
	-   Vida
	
-   Personaje (Arte + Animaciones)
	-   Idle
	-   Andar x4
	-   Pasar a mejor vida
	-   ataque base
  
-   Tilemaps de Escenario
	-   Battle stage
    -   Card Select Stage
    

-   Cartas (Item Sprite + Efecto Sprite + Animacion*)
	-   Cartas Basicas
	    -   HP (+1)
	    -   Speed (+0.25 - 0.5)
	    -   Fire Rate (+0.5)
	    -   Projectile Size (+0.5)
	    -   Range (+1)
    -   Cartas del Chaos (Efectos mas especiales)
	    -   Spreadshot (3-4-5) 
	    -   Magia Explosiva (Explota en ipacto, daño en area)
	    -   Magia de Hielo (Reduce Velocidad)
	    -   Magia de Goma (Rebota en las paredes)
	    -   Pierce (Piercing bullets)
	    -   Mini (Te hace pequeño)
	    -   Curar (Al eliminar un enemigo)
	    -   Stasis (Cuando te pegan te congelas y te vuelves invulnerable)
	    -   Magia Melee (Firaga Kingdom Hearts II)
	    -   Sugar Rush!! (Empiezas la ronda con mucha velocidad y luego te vuelves lento)
		-   Boo! (Invierte controles del enemigo)
	    -   Snail trail (Makes a trail that slows the enemy)
	    -   Gambling! (2 Cartas Random)
	    -   Donde Estoy? (al tomar daño te teletransporta a una posicion aleatoria)
	    -   Confetti (Tira Confetti)
	    -   Kaboom! (Cuando tomas daño explotas)
		-   Laser 
	    -   Dash
    

## Programacion

 *U = 1 Player Size
    

-   Main Menu
	-   Play Button
    
-   Players
	-   Movimiento: No sprint, Max speed instantaneo (10u por  segundo)
	-   Atk: Basic attack: 4u Range, 0.5s Base CD, 2x player speed
	-   HP por ronda: 3 hits
    -   Tomar Daño: Flash red, 1 Sec Invulnerability (Or until attack? Como añadido al tiempo), slight knockback (?)
    -   Death: “Ragdoll” tipo  los items de TBOI que  salen  volando.
    

-   Play Round
    -   Un jugador en cada esquina
    -   Last one standing wins (1 point added to score)
    

-   Card Round
    -   Cards in Middle of room (walk over to pick up)
    -   Every player caged
    -   Released 1 by 1 based on last round results: 4th > 3rd > 2nd > 1st
    -   0.5s between releases
    

-   Cards (All Passive)
    -   Basic Cards
	    -   HP (+1)
	    -   Speed (++50%)
	    -   Fire Rate (+50%)
	    -   Projectile Size (+50%)
	    -   Range (+50%)
    -   Chaos Cards 
	    -   Spreadshot (3-4-5  bullets) -- Keeper-T.Keeper
	    -   Exploding rounds (Explodes on impact, area damage)
	    -   Ice rounds (Makes enemies slow for 2 seconds)
	    -   Rubber rounds (Makes your bullets rebound [90 Degrees Random])
	    -   Pierce (Piercing bullets)
	    -   Tiny (You smaller and faster, but weaker)
	    -   Heal (On KILL)
	    -   Harden (No damage, but no action, activates when receiving damage, nearby enemies get repelled)
	    -   Close up magic (Enemy loses 2 health, KH2 Firaga)
	    -   Sugar Rush!! (You are really fast for 10 seconds at the start of the battle, then you go slower for 5 seconds)
	    -   Boo! (Inverts enemy's  controls on a cone radius for 3 second)
	    -   Snail trail (Makes a trail that slows the enemy)
	    -   Gambling! (Gives you 2 random cards)
	    -   Where am I? (Teleports to random location after taking damage)
	    -   Confetti (Throws confetti, nothing else)
	    -   Kaboom! (You explode when getting hit)
	    -   Brim (Usar Raycasting [https://www.youtube.com/watch?v=S6eRVwAtfOM](https://www.youtube.com/watch?v=S6eRVwAtfOM))
		-   Dash (Gatillo)
