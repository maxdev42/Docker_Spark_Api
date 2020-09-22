using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;

namespace GettingData
{
    public class Program
    {

        static void Main(string[] args)
        {
            var sparkSession = SparkSession.Builder().GetOrCreate();

            var options = new Dictionary<string, string>
            {
                {"delimiter", "|"},
                {"samplingRation", "1.0" }//,
                //{"inferSchema", "true" }
            };

            var schemaString = "invDate INT, invItemId INT, invWareHouseId INT, invQuantityOnHand String";

            var csvFileSource = sparkSession.Read()
                .Format("csv")
                .Options(options)
                .Schema(schemaString)
                .Load(args[0]);
            //.Load(@"C:\Users\maxde\source\repos\POC-Spark-Docker-NetCore\GettingData\FileSpark\inventory.dat");

            var csvFileTarget = sparkSession.Read()
                .Format("csv")
                .Options(options)
                .Schema(schemaString)
                .Load(args[1]);

            csvFileSource.PrintSchema();
            csvFileTarget.PrintSchema();

            csvFileSource.Show(5);
            csvFileTarget.Show(5);

            csvFileSource.Select("invItemId").Where("invItemId > 2124").Show();

            csvFileSource.GroupBy("invItemId").Count()
                .WithColumnRenamed("count", "total")
                .Filter("invItemId >= 2124")
                .Show();

            //Spark with SQL Queries
            csvFileSource.CreateOrReplaceTempView("csvFileSourceTemp");
            sparkSession.Sql("select invItemId from csvFileSourceTemp").Show();

            //csvFileSource.Join(csvFileTarget, "key").Show();

        }
    }
}
