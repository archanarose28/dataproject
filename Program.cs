using System;
using ConsoleTables;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Collections.Generic;


namespace CompanyMaster
{
    public class CompanyMas
    {
        public string AUTHORIZED_CAP { get; set; }
        public string DATE_OF_REGISTRATION { get; set; }

        public string PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN { get; set; }

    }
    class Program
    {

        static void Main(string[] args)
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0;  //TESTCASE1
            using (var reader = new StreamReader("C:\\Users\\ARCHANA ROSE BIJU\\Downloads\\Tripura.csv")) //TESTCASE1
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<CompanyMas>();
                        double k = Convert.ToDouble(record.AUTHORIZED_CAP);
                        if (k <= 100000)
                            val1 += 1;
                        else if (k > 100000 && k <= 1000000)
                            val2 += 1;
                        else if (k > 1000000 && k <= 10000000)
                            val3 += 1;
                        else if (k > 10000000 && k <= 100000000)
                            val4 += 1;
                        else
                            val5 += 1;

                    }   
                }

            }
            var table = new ConsoleTable("Bin", "Counts");
            table.AddRow("<= 1L", val1).AddRow(" 1L to 10L", val2).AddRow("10L to 1Cr ", val3)
                .AddRow("1Cr to 10Cr ", val4).AddRow(">10Cr ", val5);
            table.Write();


            int[] arrCount = new int[20];     //TESTCASE2
            int[] arrayYear = new int[20];
            int count;
            int Result;
            for (int i = 0, j = 2000; i < 20 && j <= 2019; i++, j++)
            {
                arrayYear[i] = j;
                arrCount[i] = 0;
            }

            //testcase2- 2000-2019 YEAR IN WHOLE LIST 

            for (int k = 0; k <= 19; k++)
            {
                count = 0;
                Result = arrayYear[k];
              

                using (var reader = new StreamReader("C:\\Users\\ARCHANA ROSE BIJU\\Downloads\\Tripura.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<CompanyMas>();

                        string dateStr = record.DATE_OF_REGISTRATION;
                        DateTime checkdate = Convert.ToDateTime(dateStr);
                        int eachyear = checkdate.Year;

                        if (eachyear == Result)
                            count += 1;
                    }

                }
                arrCount[k] = count;
        } //for
            table = new ConsoleTable("Year", "Count");

            for (int j = 0; j < 20; j++)
            {
                table.AddRow(arrayYear[j], arrCount[j]);
            }
            table.Write();

            //TESTCASE 3
            Dictionary<string, int> PRINCIPLE = new Dictionary<string, int>();
            using (var reader = new StreamReader("C:\\Users\\ARCHANA ROSE BIJU\\Downloads\\Tripura.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    var rows = csv.GetRecords<CompanyMas>();
                    foreach (var row in rows)
                    {

                        string pba = row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN;
                        string dateStrr = row.DATE_OF_REGISTRATION;
                        DateTime checkdatee = Convert.ToDateTime(dateStrr);
                        int eachyearr = checkdatee.Year;
                        if(eachyearr == 2015)
                        {
                            if (PRINCIPLE.ContainsKey(pba))
                                PRINCIPLE[pba] += 1; //PRINCIPLE[key] = value. so, PRINCIPLE[pba]+=1 increments value2
                            else  //First occurrence of string pba in 2015
                                PRINCIPLE.Add(pba, 1);
                        }
                    }  //foreach     
                }
            }
           
          
            table = new ConsoleTable("PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN ", "Count");
            foreach (KeyValuePair<string, int> kvp in PRINCIPLE)
            {
                table.AddRow(kvp.Key, kvp.Value);
            }
            table.Write();

            //TESTCASE 4

           Dictionary<int, Dictionary<string, int>> GRUOPAGGR = new Dictionary<int, Dictionary<string, int>>();
            //First adding 2000 to 2019  in dictionary to maintain the order of years in DICTIONARY
            for (int k = 2000; k <= 2019; k++)
            {
                GRUOPAGGR.Add(k, new Dictionary<string, int>());
            }
            using (var reader = new StreamReader("C:\\Users\\ARCHANA ROSE BIJU\\Downloads\\Tripura.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    var rows = csv.GetRecords<CompanyMas>();
                    foreach (var row in rows)
                    {
                        string pba = row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN;
                        string dateStrr = row.DATE_OF_REGISTRATION;
                        DateTime checkdatee = Convert.ToDateTime(dateStrr);
                        int eachyearr = checkdatee.Year;
                        if ((eachyearr >= 2000) && (eachyearr <= 2019))
                        {
                            if(!(GRUOPAGGR[eachyearr].ContainsKey(pba)))      // No single string in this year in dictionary , so add 1 , since FIRST OCCURRENCE of string
                                GRUOPAGGR[eachyearr].Add(pba, 1);
                            else if(GRUOPAGGR[eachyearr].ContainsKey(pba)) // string are already present so increment its count
                                GRUOPAGGR[eachyearr][pba]+=1;
                        }
                    }
                }
            }
            table = new ConsoleTable("PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN ", "Counts");
            foreach (int key in GRUOPAGGR.Keys)
            {
                var value1 = GRUOPAGGR[key];
                table.AddRow(key, "");
                foreach (var key2 in value1.Keys)
                {
                    table.AddRow(key2, value1[key2]);
                }
            }
                table.Write();
                Console.ReadLine();
         }
    }
}         