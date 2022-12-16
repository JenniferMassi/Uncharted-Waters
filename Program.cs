using Microsoft.VisualBasic;
using System.ComponentModel;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Emit;

namespace Uncharted_Waters {
    internal class Program {
        static void Main(string[] args) {
            //create function to get satellite data. This function is already given and all the information needed to create the other functions will be directly from this function
            //get the ratio of enemy submarines to US navy submarines
            //determine for each ocean level if the US navy submarines should attack. submarines in a level should attack if they outnumber the enemy submarines
            //display each slice of the ocean data

            //MAIN MENU OUTPUT
            MainMenu();
            //Determie what percentage of each level is populated by submarines
            //Decide the total ratio 
            int[,,] subData = GetSatelliteData();//this subData variable is holding the information created in the GetSatelliteData function. if it were assigned to a different variable then it would be different numbers bc of the random number generator
            int surface = 0;//is represented by "int level" in the GetPercentage Function. Same as the next 2 variables. This is how you'll be able to find the percentage of the 3 different levels. 0 represents the surface level
            int underWater = 1;//1 equals the 2nd level
            int deepWater = 2;//2 equals the 3rd level. THIS IS BECAUSE THERE ARE 3 LEVELS BUT IT STARTS AT 0. I TRIED SETTING ALL THREE OF THESE VARIABLES TO 0 AND IT ALL RETURNED THE SAME INFORMATION BC THE PROGRAM JUST RECOGNIZED THEM AS ALL THE SAME LEVEL



            //Makes it easier to save it to a variable
            double enemyToFriendlyShipRatio = GetEnemyShipRatio(subData);
            double enemyToFriendlySubRatioForSurface = GetEnemySubRatioForEachLevel(subData, surface);
            double enemyToFriendlySubRatioForUnderWater = GetEnemySubRatioForEachLevel(subData, underWater);
            double enemyToFriendlySubRatioForDeepWater = GetEnemySubRatioForEachLevel(subData, deepWater);

            //PERCENTAGE OF TOTAL SUBS POPULATING EACH OCEAN LEVEL
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Total Combined Submarine Percentage by Level");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"The percentage of the submarines populating the oceans surface is {GetPercentage(subData, surface)}%");
            Console.WriteLine($"The percentage of the submarines underwater are {GetPercentage(subData, underWater)}%");
            Console.WriteLine($"The percentage of the submarines populating the deep water {GetPercentage(subData, deepWater)}%");

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            //OUTPUT RATIO OF ENEMY SUBS TO NAVY SUBS 
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Enemy Submarine Data");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"The Total Ratio of Enemy submarines to Navy submarines is {enemyToFriendlyShipRatio:f2}");
            // this calls upon the int[,,]subData array as a whole, since you're not calling upon an individual array element you just list the variable name
            //the GetEnemyShipRatio function is taking the information stored from the subData function and plugging that info into the GetEnemyShipRatio Function
            //Also it's not possible to make a separate int [,,] variable for the GetEnemyShipRatio function. Example: int [,,] variable = GetEnemyShipRatio(); IS NOT POSSIBLE. This is bc the GetEnemyShipRatio function is a DOUBLE . Therefore it can only be equal to other doubles and it can only hold the arguments inside of the function. In this case the 3D array and an int.Which is why it's formatted as GetEnemyShipRatio(subdata,surface) bc subdata is = to an int[,,] and surface(and also underwater, deepwater) are an int. "AS ABOVE SO BELOW"                                                                //This gets the percentage of each of the levels (surface, underWater, & deepWater). the subData variable now holds the information created in the GetSatellite Function and will be responsible for randomizing the information for each level
        
            //Should Navy Subs attack OUTPUT
            Console.WriteLine($"Enemy Sub Ratio - Surface: {enemyToFriendlySubRatioForSurface}. Go for surface level attack: {GetAttack(subData, surface)}");
            Console.WriteLine($"Enemy Sub Ratio - Under Water: {enemyToFriendlySubRatioForUnderWater}.  Go for under water level attack: {GetAttack(subData, underWater)}");
            Console.WriteLine($"Enemy Sub Ratio - Deep Water: {enemyToFriendlySubRatioForDeepWater}. Go for deep water level attack: {GetAttack(subData, deepWater)}");
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");

            //DISPLAY SATELLITTE DATA
            DisplayLevels(subData);
            // DisplayLevels(subData, underWater);
            // DisplayLevels(subData, deepWater);


            //FUNCTION GIVEN. ALL OTHER FUNCTIONS/DATA WILL BE CREATED FROM THE INFORMATION GIVEN IN THIS FUNCTION
            static int[,,] GetSatelliteData() {
                //[,,] means this will be a 3D ARRAY
                Random rand = new Random();//this denotes that there will be a randomizer at some point
                int[,,] data = new int[10, 10, 3]; //3 represents the 3 levels above water, under water, deep level 
                                                   //rows, columns, depth/levels
                for (int z = 0; z < data.GetLength(2); z++) {//this is for levels. data.GetLength(2) <----the (2) denotes that it's a level aka the depth of the 3D array
                    for (int y = 0; y < data.GetLength(1); y++) {//this is for columns. data.GetLength(1) <------ the (1) denotes that it's a 2D array as 2D arrays have both rows AND columns
                        for (int x = 0; x < data.GetLength(0); x++) {//this is for rows. data.GetLength <----- the (0) denotes that it's only the row aka only a 1 dimensional array
                            if (rand.Next(0, 101) < 25) { //25 was picked as the parameter bc if he picked 100 then EVERY space would be occupied. By picking a lower number such as 25 that means only a small amount spaces will be occupied. the lower the number chosen the less spaces will be occupied
                                                          //If the random number generated is less than 25, that space would be filled by a sub chosen by the next random number generator.
                                data[x, y, z] = rand.Next(1, 3);//"z" MUST BE LAST BECAUSE IT'S EQUAL TO 3, IF IT'S PUT IN ANOTHER SPOT IT WILL BE EQUAL TO 10
                            }//END IF STATEMENT
                        }//END 2ND FOR LOOP
                    }//END 3RD FOR LOOP
                }//END 1ST FOR LOOP
                return data;
            }//END FUNCTION
             //the ARGUMENTS ARE THE VARIABLES INSIDE THE FUNCTION
            static double GetPercentage(int[,,] data, int level) {//this is to get the percentage of columns & rows populated with all ships (enemy & our ships)
                                                                  //empty 3D array to hold [x,y,z] aka rows,columns,levels
                                                                  //GetPercentage has to have an int[,,] array variable and an int variable to represent the level in MAIN because that's the arguments/variables inside the FUNCTION. 

                double percent = 0;
                for (int y = 0; y < data.GetLength(1); y++) { //gets the number of rows in the columns
                    for (int x = 0; x < data.GetLength(0); x++) {//gets the number of subs in the rows
                        if (data[x, y, level] != 0) {//then
                            percent += 1; //this increases the percent for that level if it's not an empty space aka not equal to zero. this only levels it up by 1

                        }//END IF STATEMENT 
                    }//END FOR LOOP
                }//END SECOND FOR LOOP
                return percent / 100;  //THERE ARE 100 SPACES IN EACH LEVEL SO YOU DIVIDE IT BY 100 TO GET THE PERCENTAGE
                                       //DOING THIS WAY WILL MAKE IT SO IT ISN'T CONTINUALLY DIVIDED BY 100 LIKE IT WOULD BE IF YOU MADE IT TO SUBTOTAL = SUBTOTAL/100
            }//END GetPercentage Function

            //Create function to determine how many enemy ships there compared to US navy ships IN ALL LEVELS
            static double GetEnemyShipRatio(int[,,] data) {//GetEnemyShipRatio is a double so it needs to be a double in MAIN as well. int[,,]data denotes that there will be a 3D array in this function
                                                           //GetEnemyShipRatio is the Information that will be OUTPUTTED and the arguments inside are what is being INPUTTED
                double enemySubs = 0;
                double navySubs = 0;
                for (int z = 0; z < data.GetLength(2); z++) {//this allows z(levels) to loop until it reaches the length of all the levels
                    for (int y = 0; y < data.GetLength(1); y++) {//this allows for y(columns) to loop until it reaches the length of the total columns
                        for (int x = 0; x < data.GetLength(0); x++) {//this allows for x(rows) to loop until it reaches the length of the total rows
                            if (data[x, y, z] == 2) {//if the 3D array 'data[x,y,z]' aka data[rows,columns,levels]  == 2 then.....
                                enemySubs += 1;//ADD an enemySub 
                            } else if (data[x, y, z] == 1) {//then
                                navySubs += 1;
                            }//END ELSE IF

                        }//END X FOR LOOP
                    }//END FOR Y LOOP
                }//END Z FOR LOOP
                return enemySubs / navySubs; //this divides enemySubs/navySubs to get the ratio so you don't have to make another variable and do the calculation there
                                             // if > than 1, more enemy subs than navy subs. if < than 1 more navy subs than enemy subs
            }//END GetEnemyShipRatio

            //Create function to determine how many enemy ships there compared to US navy ships FOR EACH LEVEL
            static double GetEnemySubRatioForEachLevel(int[,,] data, int level) {

                double enemySubs = 0;
                double navySubs = 0;
                for (int y = 0; y < data.GetLength(1); y++) {//this allows for y(columns) to loop until it reaches the length of the total columns
                    for (int x = 0; x < data.GetLength(0); x++) {//this allows for x(rows) to loop until it reaches the length of the total rows
                        if (data[x, y, level] == 2) {//if the 3D array 'data[x,y,z]' aka data[rows,columns,levels]  == 2 then.....
                            enemySubs += 1;//ADD an enemySub 
                        } else if (data[x, y, level] == 1) {//then
                            navySubs += 1;
                        }//END ELSE IF

                    }//END X FOR LOOP
                }//END FOR Y LOOP
                 // }//END Z FOR LOOP
                return enemySubs / navySubs; //this divides enemySubs/navySubs to get the ratio so you don't have to make another variable and do the calculation there
                                             // if > than 1, more enemy subs than navy subs. if < than 1 more navy subs than enemy subs
            }//END GetEnemyShipRatio

            //Create function to determine if navy ships should attack
            static bool GetAttack(int[,,] data, int level) {
                //int[,,] to input the 3D ARRAY data already previously declared in MAIN, GetEnemyShipRatio function into this function in MAIN, level to seperate the levels
                //so you're going to have to find out the enemy ship to navy ship ratio for EACH LEVEL. if the enemy ship ratio is LESS than the navy ship on each level then bool = true aka ATTACK
                //Create NESTED FOR LOOPS TO GET COLUMNS AND ROWS. NO NEED TO DO ONE FOR THE LEVELS BC THE VARIABLE level is here 
                bool attack = true;
                double attackRatio = GetEnemySubRatioForEachLevel(data, level);//the int[,,] data is plugged into the GetEnemyShipRatio Function and converted to a variable so we can use it in the IF STATEMENT below

                if (attackRatio >= 1) {//the 1 is here because it's a 1:1 ratio. if attackRatio (which is really the ratio of enemy ships to navy ships) is greater than 1 (aka 1:1 ratio) then navy ships shouldn't attack
                    attack = false;
                } else {
                    attack = true;
                }

                return attack;
            }//End GetAttack Function

            static void DisplayLevels(int[,,] data) {
              
              
                //int[,,] subData = GetSatelliteData();
                string[] levelNames = new string[3];
                string[] levelNamesLiteral = { "SURFACE", "UNDER WATER", "DEEP WATER" };
                //0         1               2
                for (int z = 0; z < data.GetLength(2); z += 1) {//
                   if(z == 0) {//then

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;

                    }//END IF STATEMENT
                   else if(z == 1) { //then
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    }//END ELSE IF
                   else {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }//END ELSE STATEMENT
                    Console.WriteLine($"----{levelNamesLiteral[z]}-------");
                    for (int y = 0; y < data.GetLength(1); y += 1) {//
                        for (int x = 0; x < data.GetLength(0); x += 1) {//
                            int allLevels = data[x, y, z];  //get item from array
                            Console.Write($"{allLevels} ");  //write to console
                        }//end for
                        Console.WriteLine();
                    }//end for//

                }//end for//

            }//
        }//END DisplayLevels Function
        static void MainMenu() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*****UNCHARTED WATERS*****");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
           // Console.WriteLine("----------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("The US Navy implements satellite technology to detect above water, underwater, and deep-water submarines.");
            Console.WriteLine("These satellites scan for both US Navy submarines and Enemy submarines.");
            Console.WriteLine("The satellite scan data determines whether or not the US Navy should attack Enemy submarines.");
            Console.WriteLine("US Navy subs should attack if they outnumber Enemy subs");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The location of Navy subs are represented by '1' on the satellite scan.");
            Console.WriteLine("The location of Enemy subs are represented by '2' on the satellite scan");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
           // Console.WriteLine("----------------------------------------");
            Console.WriteLine("PRESS ENTER TO VIEW SATELLITE DATA");
            Console.ReadLine();
            Console.Clear();
          }//END MainMenu Function
        }//END CLASS
}//END NAMESPACE