using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstProject
{
    internal class Program
    {
        static bool _isCommandPageOpen = false;

        static void Main(string[] args)
        {
            // test
            Console.WriteLine(" Welcome!");
            NewLine();
            List<Exercise> toDoList = new List<Exercise>();

            PrintList(toDoList);
            NewLine();
            OpenCommands();
            NewLine();

            Console.WriteLine("Enter one of the operations for task");
            while (true)
            {
                var input = Console.ReadLine();
                var processing = input.Split();
                if (processing.Length == 2)
                {
                    input = processing[0];
                }
                switch (input)
                {
                    case "print":
                        PrintList(toDoList);
                        break;

                    case "edit":                     
                        RunEditCommand(processing[1],toDoList);
                        break;

                    case "add":
                        RunAddCommand(toDoList);
                        break;

                    case "delete":
                        RunDeleteCommand(processing[1], toDoList);
                        break;

                    case "clear":
                        RunClearCommand(toDoList);
                        break;
                    default:
                        PrintCommandNotExist();
                        break;
                }
            }
            Console.ReadLine();


        }

        static void RunAddCommand(List<Exercise> toDoList)
        {
            Console.WriteLine("Write exercise name");
            var input = Console.ReadLine();

            toDoList.Add(new Exercise(input,GetNextId(toDoList)));
        }
        static void RunEditCommand(string id, List<Exercise> toDoList)
        {
            bool isExist = false;
            foreach(var element in toDoList )
            {
                if(element.ID == id)
                {
                    isExist= true; 
                    break;
                }
            }

            if (isExist == false)
            {
                Console.WriteLine("Wrong task Id");
                return;
            }


            Console.WriteLine("You change this name");
            var newName = Console.ReadLine();
            
             for(int i = 0; i < toDoList.Count; i++)
             {
                if (toDoList[i].ID == id)
                {
                    toDoList[i].Name = newName;
                    break;
                }
             }
            Console.WriteLine("Edit for sacceesful");
                
            
           

        }
        static void RunDeleteCommand(string id,List<Exercise> toDoList)
        {
            Console.WriteLine("Are you sure do delete? Y/N");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "Y")
                {
                    toDoList.RemoveAt(1);
                }else if (input == "N")
                {
                    break;
                }
                else
                {
                    PrintCommandNotExist();
                }
            }      
        }
        static void RunClearCommand(List<Exercise> toDoList)
        {
            Console.WriteLine("Are you sure do delete? Y/N");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "Y")
                {
                    toDoList.Clear();
                }
                else if (input == "N")
                {
                    break;
                }
                else
                {
                    PrintCommandNotExist();
                }
            }
         }

        static string GetNextId(List<Exercise> toDoList)
        {
            var lastElement = toDoList.LastOrDefault();

            if (lastElement == null)
            {
                return "1";
            }
            
            int lastElementId = int.Parse(lastElement.ID);
            return (lastElementId+1).ToString();
        }

        static void NewLine()
        {
            Console.WriteLine();
        }

        static void OpenCommands()
        {
            Console.WriteLine("Enter cmd to see other operations");
            while (true)
            {
                var input = Console.ReadLine();

                if (input == Commands.cmd.ToString())
                {
                    PrintCommand();
                    _isCommandPageOpen = true;
                    break;
                }

                else
                {
                    PrintCommandNotExist();
                    NewLine();
                }
            }
        }

        static void PrintCommandNotExist()
        {
            Console.WriteLine("Command not exist");
        }


        static void PrintList(List<Exercise> list)
        {
            Console.WriteLine("This is ToDoList");
            if (!list.Any())
            {
                Console.WriteLine("You do not have something today");
                return; //early return
            }

            foreach (Exercise exercise in list)
            {
                Console.WriteLine(exercise.ToString());
            }
        }

        static void PrintCommand()
        {
            var commandString = "1. Print ToDoList (print)" + "\n" +
                                "2. Edit EditTask (edit <taskId>)" + "\n" +
                                "3. Add AddTask (add )" + "\n" +
                                "4. Delete DeleteTask (delete <taskId>)" + "\n" +
                                "5. Clear ToDoList (clear)";
            Console.WriteLine(commandString);
        }
        class Exercise
        {
            public string Name { get; set; }
            public string ID { get; set; }

            public Exercise(string name, string id)
            {
                Name = name;
                ID = id;
            }

            public override string ToString()
            {
                return $"{ID} - {Name}";
            }

        }

        enum Commands : byte
        {
            cmd,
            print,
            edit,
            add,
            delete,
            clear
        }
    }
}
