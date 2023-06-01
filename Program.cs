using System.Text;
using CM_ADS;
using CM_ADS.CW;

class Program
{
    public static void Main()
    {
        StringBuilder taskString = new StringBuilder();
        taskString.AppendLine("Выберите предмет:");
        taskString.AppendLine("1) Численные методы");
        taskString.AppendLine("2) Алгоритмы и структуры данных");
        taskString.AppendLine("3) Курсовая");
        Console.WriteLine(taskString);
        
        String input = Console.ReadLine();
        Console.Clear();

        switch (input) {
            case "1":
                TestCM.Test();
                break;
            
            case "2":
                TestADS.Test();
                break;
            
            case "3":
                
                TestCourseWork.TestCW();
                break;
            
            default:
                return;
        }
    }
}