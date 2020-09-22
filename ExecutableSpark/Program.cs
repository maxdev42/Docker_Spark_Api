using System;
using System.Diagnostics;


namespace ExecutableSpark
{
    class Program
    {
        static void Main(string[] args)
        {

            ProcessStartInfo infos = new ProcessStartInfo();
            infos.FileName = "cmd.exe";
            infos.Arguments = @"/c cd ..\..\..\..\GettingData\bin\Debug\netcoreapp3.1 && dotnet build ..\..\..\GettingData.csproj && spark-submit --class org.apache.spark.deploy.dotnet.DotnetRunner --master local microsoft-spark-2.4.x-0.12.1.jar dotnet GettingData.dll";

            Process cmd = new Process();
            cmd.StartInfo = infos;

            cmd.Start();
        }
    }
}
