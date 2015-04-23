using System;

namespace GCM.BL.Core
{
    public static class SolutionManager
    {
        public static void Run()
        {
            Console.WriteLine(@"To quit enter ""q"" or ""Q"" and press ""Enter"" ");
            string exitChars = "qQ";
            bool done = false;

            while (!done)
            {
                //get input
                string input = Console.ReadLine().Replace(" ", string.Empty);

                //check if quit was selected
                if (exitChars.Contains(input))
                {
                    done = true;
                    continue;
                }

                //validate input data
                if (!input.Trim().IsValidInput())
                {
                    Console.WriteLine("Input data are not in expected format!\n");
                    continue;
                }

                IBusinessProcess core = new BusinessProcess(input);
                Console.WriteLine("output:\t{0}\n", core.CreateOutput());
            }
        }

    }
}
