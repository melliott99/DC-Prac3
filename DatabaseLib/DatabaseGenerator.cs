using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    /*Used to create lists of each field for the database*/
    internal class DatabaseGenerator
    {
        Random r = new Random();
        List<string> firstNames;
        List<string> lastNames;
        List<uint> pins = new List<uint>();
        List<uint> accNums = new List<uint>();
        List<int> balances = new List<int>();
        List<string> images = new List<string>();

        public static void Main(string[] args)
        {
            DatabaseClass dbc = DatabaseClass.GetInstance();
        }
        public DatabaseGenerator()
        {
            createList();
        }

        public DataStruct GetEntry()
        {
            DataStruct data = new DataStruct(GetFirstName(), GetLastName(), GetPin(), GetAcctNo(), GetBalance(), GetImage());
            return data;
        }
         
        private string GetFirstName()
        {
            //Randomly selectes a first name from the list and returns it
            return firstNames[r.Next(0,firstNames.Count())];
        }

        private string GetLastName()
        {
            //Randomly selects a last name from the list and returns it
            return lastNames[r.Next(0, lastNames.Count())];
        }

        private uint GetPin()
        {
            //Randomly selects pin from a bunnch of random pins
            return pins[r.Next(0, pins.Count())];
        }

        private uint GetAcctNo()
        {
            //randomly selects an account number from a list of account numbers
            return accNums[r.Next(0, accNums.Count())];
        }

        private int GetBalance()
        {
            //Randomly selects a balance from a list of balances
            return balances[r.Next(0, balances.Count())];
        }


        /*Comes from this link
         * https://stackoverflow.com/questions/7350679/convert-a-bitmap-into-a-byte-array
         * Allows me to get any photos from that directory which are called numbers from 1-6
         */
        public byte[] GetImage()
        {
            int x = r.Next(1, 6);
            //Generates a random number from 1-6 which is how many photos that are offered
            //That photo is then opened
                      
            Bitmap photo = new Bitmap(@"C:\CodePictures\" + x + ".png", true);
          
            return ImageToByte(photo);
        }

        //Converts the image to byte array
        private byte[] ImageToByte(Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance, out byte[] image)
        {
            
            pin = GetPin();
            acctNo = GetAcctNo();
            firstName = GetFirstName();
            lastName = GetLastName();
            balance = GetBalance();
            image = GetImage();
        }

        /*Creates the list of all the fields*/
        public void createList()
        {
            firstNames = new List<string>(){
                "Aaren", "Aarika", "Abagael", "Abagail", "Abbe", "Abbey", "Abbi", "Abbie", "Abby", "Abbye",
                "Abigael", "Abigail", "Abigale", "Abra", "Ada", "Adah", "Adaline", "Adan", "Adara", "Adda",
                "Addi", "Addia", "Addie", "Addy", "Adel", "Adela", "Adelaida", "Adelaide", "Adele", "Adelheid",
                "Adelice", "Adelina", "Adelind", "Adeline", "Adella", "Adelle", "Adena", "Adey", "Adi", "Adiana",
                "Adina", "Adora", "Adore", "Adoree", "Adorne", "Adrea", "Adria", "Adriaens", "Adrian", "Adriana",
                "Adriane", "Adrianna", "Adrianne", "Adriena", "Adrienne", "Aeriel", "Aeriela", "Aeriell", "Afton", "Ag",
                "Agace", "Agata", "Agatha", "Agathe", "Aggi", "Aggie", "Aggy", "Agna", "Agnella", "Agnes",
                "Agnese", "Agnesse", "Agneta", "Agnola", "Agretha", "Aida", "Aidan", "Aigneis", "Aila", "Aile",
                "Ailee", "Aileen", "Ailene", "Ailey", "Aili", "Ailina", "Ailis", "Ailsun", "Ailyn", "Aime",
                "Aimee", "Aimil", "Aindrea", "Ainslee", "Ainsley", "Ainslie", "Ajay", "Alaine", "Alameda", "Alana",
                "Alanah", "Alane", "Alanna", "Alayne", "Alberta", "Albertina", "Albertine", "Albina", "Alecia", "Aleda",
                "Aleece", "Aleen", "Alejandra", "Alejandrina", "Alena", "Alene", "Alessandra", "Aleta", "Alethea", "Alex",
                "Alexa", "Alexandra", "Alexandrina", "Alexi", "Alexia", "Alexina", "Alexine", "Alexis", "Alfi", "Alfie",
                "Alfreda", "Alfy", "Ali", "Alia", "Alica", "Alice", "Alicea", "Alicia", "Alida", "Alidia",
                "Alie", "Alika", "Alikee", "Alina", "Aline", "Alis", "Alisa", "Alisha", "Alison", "Alissa",
                "Alisun", "Alix", "Aliza", "Alla", "Alleen", "Allegra", "Allene", "Alli", "Allianora", "Allie",
                "Allina", "Allis", "Allison", "Allissa", "Allix", "Allsun", "Allx", "Ally", "Allyce", "Allyn",
                "Allys", "Allyson", "Alma", "Almeda", "Almeria", "Almeta", "Almira", "Almire", "Aloise", "Aloisia",
                "Aloysia", "Alta", "Althea", "Alvera", "Alverta", "Alvina", "Alvinia", "Alvira", "Alyce", "Alyda",
                "Alys", "Alysa", "Alyse", "Alysia", "Alyson", "Alyss", "Alyssa", "Amabel", "Amabelle", "Amalea",
                "Amalee", "Amaleta", "Amalia", "Amalie", "Amalita", "Amalle", "Amanda", "Amandi", "Amandie", "Amandy",
                "Amara", "Amargo", "Amata", "Amber", "Amberly", "Ambur", "Ame", "Amelia", "Amelie", "Amelina",
                "Ameline", "Amelita", "Ami", "Amie", "Amii", "Amil", "Amitie", "Amity", "Ammamaria", "Amy",
                "Amye", "Ana", "Anabal", "Anabel", "Anabella", "Anabelle", "Analiese", "Analise", "Anallese", "Anallise",
                "Anastasia", "Anastasie", "Anastassia", "Anatola", "Andee", "Andeee", "Anderea", "Andi", "Andie", "Andra",
                "Andrea", "Andreana", "Andree", "Andrei", "Andria", "Andriana", "Andriette", "Andromache", "Andy", "Anestassia",
                "Anet", "Anett", "Anetta", "Anette", "Ange", "Angel", "Angela", "Angele", "Angelia", "Angelica",
                "Angelika", "Angelina", "Angeline", "Angelique", "Angelita", "Angelle", "Angie", "Angil", "Angy", "Ania",
                "Anica", "Anissa", "Anita", "Anitra", "Anjanette", "Anjela", "Ann", "Ann-Marie", "Anna", "Anna-Diana",
                "Anna-Diane", "Anna-Maria", "Annabal", "Annabel", "Annabela", "Annabell", "Annabella", "Annabelle", "Annadiana", "Annadiane",
                "Annalee", "Annaliese", "Annalise", "Annamaria", "Annamarie", "Anne", "Anne-Corinne", "Anne-Marie", "Annecorinne", "Anneliese",
                "Annelise", "Annemarie", "Annetta", "Annette", "Anni", "Annice", "Annie", "Annis", "Annissa", "Annmaria",
                "Annmarie", "Annnora", "Annora", "Anny", "Anselma", "Ansley", "Anstice", "Anthe", "Anthea", "Anthia",
                "Anthiathia", "Antoinette", "Antonella", "Antonetta", "Antonia", "Antonie", "Antonietta", "Antonina", "Anya", "Appolonia",
                "April", "Aprilette", "Ara", "Arabel", "Arabela", "Arabele", "Arabella", "Arabelle", "Arda", "Ardath",
                "Ardeen", "Ardelia", "Ardelis", "Ardella", "Ardelle", "Arden", "Ardene", "Ardenia", "Ardine", "Ardis",
                "Ardisj", "Ardith", "Ardra", "Ardyce", "Ardys", "Ardyth", "Aretha", "Ariadne", "Ariana", "Aridatha",
                "Ariel", "Ariela", "Ariella", "Arielle", "Arlana", "Arlee", "Arleen", "Arlen", "Arlena", "Arlene",
                "Arleta", "Arlette", "Arleyne", "Arlie", "Arliene", "Arlina", "Arlinda", "Arline", "Arluene", "Arly",
                "Arlyn", "Arlyne", "Aryn", "Ashely", "Ashia", "Ashien", "Ashil", "Ashla", "Ashlan", "Ashlee",
                "Ashleigh", "Ashlen", "Ashley", "Ashli", "Ashlie", "Ashly", "Asia", "Astra", "Astrid", "Astrix",
                "Atalanta", "Athena", "Athene", "Atlanta", "Atlante", "Auberta", "Aubine", "Aubree", "Aubrette", "Aubrey",
                "Aubrie", "Aubry", "Audi", "Audie", "Audra", "Audre", "Audrey", "Audrie", "Audry", "Audrye",
                "Audy", "Augusta", "Auguste", "Augustina", "Augustine", "Aundrea", "Aura", "Aurea", "Aurel", "Aurelea",
                "Aurelia", "Aurelie", "Auria", "Aurie", "Aurilia", "Aurlie", "Auroora", "Aurora", "Aurore", "Austin",
                "Austina", "Austine", "Ava", "Aveline", "Averil", "Averyl", "Avie", "Avis", "Aviva", "Avivah",
                "Avril", "Avrit", "Ayn", "Bab", "Babara", "Babb", "Babbette", "Babbie", "Babette", "Babita",
                "Babs", "Bambi", "Bambie", "Bamby", "Barb", "Barbabra", "Barbara", "Barbara-Anne", "Barbaraanne", "Barbe",
                "Barbee", "Barbette", "Barbey", "Barbi", "Barbie", "Barbra", "Barby", "Bari", "Barrie", "Barry",
                "Basia", "Bathsheba", "Batsheva", "Bea", "Beatrice", "Beatrisa", "Beatrix", "Beatriz", "Bebe", "Becca",
                "Becka", "Becki", "Beckie", "Becky", "Bee", "Beilul", "Beitris", "Bekki", "Bel", "Belia",
                "Belicia", "Belinda", "Belita", "Bell", "Bella", "Bellanca", "Belle", "Bellina", "Belva", "Belvia",
                "Bendite", "Benedetta", "Benedicta", "Benedikta", "Benetta", "Benita", "Benni", "Bennie", "Benny", "Benoite",
                "Berenice", "Beret", "Berget", "Berna", "Bernadene", "Bernadette", "Bernadina", "Bernadine", "Bernardina", "Bernardine",
                "Bernelle", "Bernete", "Bernetta", "Bernette", "Berni", "Bernice", "Bernie", "Bernita", "Berny", "Berri",
                "Berrie", "Berry", "Bert", "Berta", "Berte", "Bertha", "Berthe", "Berti", "Bertie", "Bertina"
                };


            lastNames = new List<string>(){"SMITH", "JOHNSON", "WILLIAMS", "JONES", "BROWN", "DAVIS", "MILLER", "WILSON", "MOORE", "TAYLOR",
                            "ANDERSON", "THOMAS", "JACKSON", "WHITE", "HARRIS", "MARTIN", "THOMPSON", "GARCIA", "MARTINEZ", "ROBINSON",
                            "CLARK", "RODRIGUEZ", "LEWIS", "LEE", "WALKER", "HALL", "ALLEN", "YOUNG", "HERNANDEZ", "KING",
                            "WRIGHT", "LOPEZ", "HILL", "SCOTT", "GREEN", "ADAMS", "BAKER", "GONZALEZ", "NELSON", "CARTER",
                            "MITCHELL", "PEREZ", "ROBERTS", "TURNER", "PHILLIPS", "CAMPBELL", "PARKER", "EVANS", "EDWARDS", "COLLINS",
                            "STEWART", "SANCHEZ", "MORRIS", "ROGERS", "REED", "COOK", "MORGAN", "BELL", "MURPHY", "BAILEY",
                            "RIVERA","COOPER","RICHARDSON","COX","HOWARD","WARD","TORRES","PETERSON","GRAY","RAMIREZ",
                                "JAMES","WATSON","BROOKS","KELLY","SANDERS","PRICE","BENNETT","WOOD","BARNES","ROSS",
                                "HENDERSON","COLEMAN","JENKINS","PERRY","POWELL","LONG","PATTERSON","HUGHES","FLORES","WASHINGTON",
                                "BUTLER","SIMMONS","FOSTER","GONZALES","BRYANT","ALEXANDER","RUSSELL","GRIFFIN","DIAZ","HAYES",
                                "MYERS","FORD","HAMILTON","GRAHAM","SULLIVAN","WALLACE","WOODS","COLE","WEST","JORDAN",
                                "OWENS","REYNOLDS","FISHER","ELLIS","HARRISON","GIBSON","MCDONALD","CRUZ","MARSHALL","ORTIZ",
                                "GOMEZ","MURRAY","FREEMAN","WELLS","WEBB","SIMPSON","STEVENS","TUCKER","PORTER","HUNTER",
                                "HICKS","CRAWFORD","HENRY","BOYD","MASON","MORALES","KENNEDY","WARREN","DIXON","RAMOS",
                                "REYES","BURNS","GORDON","SHAW","HOLMES","RICE","ROBERTSON","HUNT","BLACK","DANIELS",
                                "PALMER","MILLS","NICHOLS","GRANT","KNIGHT","FERGUSON","ROSE","STONE","HAWKINS","DUNN",
                                "PERKINS","HUDSON","SPENCER","GARDNER","STEPHENS","PAYNE","PIERCE","BERRY","MATTHEWS","ARNOLD",
                                "WAGNER","WILLIS","RAY","WATKINS","OLSON","CARROLL","DUNCAN","SNYDER","HART","CUNNINGHAM",
                                "BRADLEY","LANE","ANDREWS","RUIZ","HARPER","FOX","RILEY","ARMSTRONG","CARPENTER","WEAVER",
                                "GREENE","LAWRENCE","ELLIOTT","CHAVEZ","SIMS","AUSTIN","PETERS","KELLEY","FRANKLIN","LAWSON",
                                "FIELDS","GUTIERREZ","RYAN","SCHMIDT","CARR","VASQUEZ","CASTILLO","WHEELER","CHAPMAN","OLIVER",
                                "MONTGOMERY","RICHARDS","WILLIAMSON","JOHNSTON","BANKS","MEYER","BISHOP","MCCOY","HOWELL","ALVAREZ",
                                "MORRISON","HANSEN","FERNANDEZ","GARZA","HARVEY","LITTLE","BURTON","STANLEY","NGUYEN","GEORGE",
                                "JACOBS","REID","KIM","FULLER","LYNCH","DEAN","GILBERT","GARRETT","ROMERO","WELCH",
                                "LARSON","FRAZIER","BURKE","HANSON","DAY","MENDOZA","MORENO","BOWMAN","MEDINA","FOWLER",
                                "BREWER","HOFFMAN","CARLSON","SILVA","PEARSON","HOLLAND","DOUGLAS","FLEMING","JENSEN","VARGAS",
                                "BYRD","DAVIDSON","HOPKINS","MAY","TERRY","HERRERA","WADE","SOTO","WALTERS","CURTIS",
                                "NEAL","CALDWELL","LOWE","JENNINGS","BARNETT","GRAVES","JIMENEZ","HORTON","SHELTON","BARRETT",
                                "OBRIEN","CASTRO","SUTTON","GREGORY","MCKINNEY","LUCAS","MILES","CRAIG","RODRIQUEZ","CHAMBERS",
                                "HOLT","LAMBERT","FLETCHER","WATTS","BATES","HALE","RHODES","PENA","BECK","NEWMAN",
                                "HAYNES","MCDANIEL","MENDEZ","BUSH","VAUGHN","PARKS","DAWSON","SANTIAGO","NORRIS","HARDY",
                                "LOVE","STEELE","CURRY","POWERS","SCHULTZ","BARKER","GUZMAN","PAGE","MUNOZ","BALL",
                                "KELLER","CHANDLER","WEBER","LEONARD","WALSH","LYONS","RAMSEY","WOLFE","SCHNEIDER","MULLINS",
                                "BENSON","SHARP","BOWEN","DANIEL","BARBER","CUMMINGS","HINES","BALDWIN","GRIFFITH","VALDEZ",
                                "HUBBARD","SALAZAR","REEVES","WARNER","STEVENSON","BURGESS","SANTOS"
                            };
          
            for(int i = 0; i < 100000; i++)
            {
                pins.Add((uint)r.Next(1000, 9999));//Want it to be 4 random numbers
                accNums.Add((uint)r.Next(100000, 999999));//Want it to be 6 random numbers
                balances.Add(r.Next(0, 10000000));//Can be any number but capped at 10000000 for the simulation

            }

           
        }
    }
    
}
