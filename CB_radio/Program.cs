using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CB_radio
{
  internal class Program
  {
    static int AtszamolPercre(int ora, int perc)
    {
      int percben = ora * 60 + perc;
      
      return percben;
    }
    
    
    
    public static void Main(string[] args)
    {
            /*
            PZG - CB-rádió
            PZG - 2025.02.21
            */

      string fejlec = "PZG - CB-rádió";
      Console.WriteLine(fejlec);
      for (int i = 0; i < fejlec.Length; i++) Console.Write('-');
      Console.WriteLine();
      
      
      StreamReader be = new StreamReader("cb.txt");
      List<string[]> radio = new List<string[]>();
      string line = be.ReadLine(); //fejlec
      line = be.ReadLine();
      while (line != null)
      {
        string[] resz = line.Split(';');
        radio.Add(resz);
        line = be.ReadLine();
      }
      be.Close();
      //3
      Console.WriteLine($"3. feladat: Bejegyzések száma: {radio.Count} db");
      
      //4
      bool volt = false;
      for (int i = 0; i < radio.Count; i++)
      {
        if (radio[i][2] == "4")
        {
          volt = true;
          break;
        }
      }

      if (volt)
      {
        Console.WriteLine($"4. feladat: Volt négy adást indító sofőr.");
      }
      else
      {
        Console.WriteLine($"4. feladat: Nem volt négy adást indító sofőr.");
      }
      
      //5
      Console.Write($"5. feladat: Kérek egy nevet: ");
      string nev = Console.ReadLine();
      volt = false;
      int hivas_db = 0;
      for (int i = 0; i < radio.Count; i++)
      {
        if (radio[i][3] == nev)
        {
          volt = true;
          hivas_db += Convert.ToInt32(radio[i][2]);
        }
      }
      
      if (volt)
        Console.WriteLine($"\t{nev} {hivas_db}x használta a CB-rádiót.");
      if (!volt)
        Console.WriteLine("\tNincs ilyen nevű sofőr!");
            
      //7
      StreamWriter ki = new StreamWriter("cb2.txt");
      
      ki.WriteLine("Kezdes;Nev;AdasDb");
      for (int i = 0; i < radio.Count; i++)
      {
        ki.WriteLine($"{AtszamolPercre(Convert.ToInt32(radio[i][0]), Convert.ToInt32(radio[i][1]))};{radio[i][3]};{radio[i][2]}");
      }
      ki.Close();
      
      //8
      HashSet<string> nevek = new HashSet<string>{};
      for (int i = 0; i < radio.Count; i++)
      {
        nevek.Add(radio[i][3]);
      }
      Console.WriteLine($"8. feladat: Sofőrök száma: {nevek.Count} fő");
            
      //9
      int legtobb_hivas = 0;
      string legtobb_hivas_nev = "";
      List<string> nevek_lista = nevek.ToList();
      for (int i = 0; i < nevek_lista.Count; i++)
      {
        hivas_db = 0;
        for (int j = 0; j < radio.Count; j++)
        {
          if (nevek_lista[i] == radio[j][3])
          {
            hivas_db += Convert.ToInt32(radio[j][2]);
          }
        }

        if (hivas_db > legtobb_hivas)
        {
          legtobb_hivas = hivas_db;
          legtobb_hivas_nev = nevek_lista[i];
        }
      }

      Console.WriteLine("9. feladat: Legtöbb adást indító sofőr");
      Console.WriteLine($"\tNév: {legtobb_hivas_nev}");
      Console.WriteLine($"\tAdások száma: {legtobb_hivas} alkalom");

      Console.WriteLine();
      Console.WriteLine("Kilépéshez nyomja meg az ENTER billentyűt!");
      Console.ReadLine();
    }
  }
}