using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Windows.Markup;

using System.Diagnostics;

public class Key_Decrypt{
    public static Dictionary<char,string> decDictionary = new Dictionary<char,string>();
    public static void Decrypt_Key_Dictionary(List<int> Key_List){
      // int[] key_array = Key_List.ToArray();
       int i=0;
       
        foreach(var a in Encrypte.uppercaseLetters){
            
            decDictionary[a] = Encrypte.encrypte_Upper[Key_List[i]];
            i++;
        }   

        foreach(var a in Encrypte.lowercaseLetters){
            
            decDictionary[a] = Encrypte.encrypte_Lower[Key_List[i] -100];
            i++;
           
        }
       
        foreach(var a in Encrypte.symbols){
            
            decDictionary[a] = Encrypte.encrypte_Symbols[Key_List[i] -200];
            i++;
            
        }

        
        foreach(var a in Encrypte.numbers){
            
            decDictionary[a] = Encrypte.encrypte_Number[Key_List[i] -300];
            i++;
        }     
      //  decDictionary['\n'] = Encrypte.encrpyte_newline;

    }


    public static List<int> EncrypteList(List<int> inputList)
    {
        var encryptedList = new List<int>();
        foreach (var y in inputList)
        {
            int x =y;
           
           x=x+1; //first step

            x=25*x; // second step

              //thirth step

                x=x+60249; 
                
                x=x-63043;

                x=x*5;

                x=x-12;



            encryptedList.Add(x);
        }
        return encryptedList;
    }


    // Decrypt fun...
    public static List<int> DecrypteList(List<int> encryptedList)
    {
        var decryptedList = new List<int>();
        foreach (var y in encryptedList)
        {
            int x = y;


                x = x + 12;          // x=x-12 
    x = x / 5;           // x=x*5 
    x = x + 63043;       // x=x-63043 
    x = x - 60249;       // x=x+60249 
    x = x / 25;          // x=25*x 
    x = x - 1;           // x=x+1 
               

                         


            decryptedList.Add(x);
        }
        return decryptedList;
    }

    //
   public static int ReverseStep(int xNew)
{
    int a = 1;
    int b = 3;
    int c = -xNew;

    //
    int discriminant = b * b - 4 * a * c;

    // 
    if (discriminant < 0)
    {
        throw new ArgumentException("Error.");
    }

    // 
    int sqrtDiscriminant = (int)Math.Sqrt(discriminant);

    // 
    if (sqrtDiscriminant * sqrtDiscriminant != discriminant)
    {
        throw new ArgumentException("Error.");
    }

    // 
    int x1 = (-b + sqrtDiscriminant) / (2 * a);
    int x2 = (-b - sqrtDiscriminant) / (2 * a);

    if ((-b + sqrtDiscriminant) % (2 * a) == 0)
    {
        return x1;
    }
    else if ((-b - sqrtDiscriminant) % (2 * a) == 0)
    {
        return x2;
    }
    else
    {
        throw new ArgumentException("error!.");
    }
}



}

