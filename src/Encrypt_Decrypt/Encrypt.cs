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

public class Encrypte
{
    public static string default_key_string= @"31,19,8,6,10,13,32,2,18,27,25,4,16,26,23,24,1,11,30,15,5,3,22,17,12,14,29,28,9,7,20,0,21,127,122,112,125,132,130,116,111,114,126,101,106,119,104,107,124,102,131,100,117,123,103,110,118,128,109,129,105,113,108,115,120,121,225,221,228,224,223,206,230,213,226,205,200,222,220,210,203,208,216,207,204,211,209,215,214,229,219,202,227,217,201,212,218,306,307,308,301,302,303,305,304,300,309";
    public static List<int> default_key;
    
    public static  List<string> encrypted_key_array_string = new List<string>();
    public static byte[] iv = Convert.FromBase64String("uQ2NXxCOty9iOIRuxH0WzA==");
   public static byte[] key = Convert.FromBase64String("2Tv05SdVanBh+sDdm91tzZbbie3SrNDid67DQT+pqvA=");
    public static byte[] bytes = new byte[1024];

    //public static List<int> tempArray = new List<int>();
    public static List<int> global_key = new List<int>();

    public static Dictionary<char,string> toEncrypteDictionary = new Dictionary<char,string>();
    public static char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇŞİÜÖĞƏabcdefghijklmnopqrstuvwxyzçşüıöğə0123456789.!@#%^&()_-+=|\\;:',?/№*${} ][<>".ToCharArray();
    public static string encrpyte_newline ="Uu_5D";

    public static string encrypte_dirnaq = "Dc_5D";

   public static string unrecognized = "u_7_n";
   public static char[] uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇŞİÜÖĞƏ".ToCharArray();
    public static char[] lowercaseLetters = "abcdefghijklmnopqrstuvwxyzçşüıöğə".ToCharArray();
    public static char[] numbers = "0123456789".ToCharArray();
    public static char[] symbols = ".!${}@#%^&()_-+=|\\;:',?/№* ][<>".ToCharArray(); 
    


public static string[] encrypte_Lower = {
    "a+s.a", "q+d.a", "q.afb", "Aq.gA", "bfd.a", "w-.jk", "e_d.d", "eh3.q", "e+08r", "e.4tf", "e.w5y", "5.qaa",
    "1_q5d", "+q.wf", "_-qh2", "-aa+a", "q-hKf", "wF.Zq", "eQ.o.", "_-qad", ".q.ag", "--dJa", "p.o.q",
    "i_w.1", "b.oa.", "c.k-k", "Ka.qb", "kKx.1", "p.ot5", "w.q1a", "vK.R2", "HJ.a2" , "k_9x1" ,"JFd_8" ,"34Fsa" ,"Sa03F" ,"Ba.S_",".SAm_" ,"LaAm_"
};

public static string[] encrypte_Upper = {
    "qAS.a", "qAD.A", "q.AAF", "qAA.g", "Af.Da", "w.J.a", "eAdAD", "eH2.Q", "e2.R3", "e.T.b", "e.Yd.", "-qAaA",
    "qADQa", "SAqAF", "SAqAH", "AKaAa", "K_9X1" ,"JFD_8" ,"34FsA" ,"SA03F" ,"BA.S_",".SAM_" ,"LAAm_", "qA-HA", "WA.qL", "OAeAO", ".AqAd", "qA.AL", "-dAMA", "pB.AE",
    "AvAWA", "VAbAO", "AVna.", "KnA.2", "LaAKa", "ACAX3", "tA_AA", "qAQ2A", "VAa.3", "AaAAb"
};

public static string[] encrypte_Number = {
    "k-at.", "k.f.a", "g-.AF", "l-lkA", ".f-4a", "xPAg2", "mAtA5", "dAn.4", "pAF.2", "zAr.a"
};

public static string[] encrypte_Symbols = {
    "rAkAa", "rAafA", "rAa.a", "dCAaA", "a_1Ad", "g.fA4", "_q.wa", "lk.Ad", "vAAd_", "dAFd3",
    "aA2dA", "d.gA5", "fOAr1", "mJAk2", "nAaAb", "cAxa4", "lAma5", "pAda3", "qTAd4", "jAaYA",
    "xLAdA", "vRAc1", "bCAq2", "zXAr3" , "zxXzR","XXDas", "xN_Nx" ,"NxN_X" ,"u_5_n","uB7_n" ,"B_67n"
};
  


public static int temp_n = 0;
public static bool RandomUsing(Random a)
{
    int n;
    if (temp_n < uppercaseLetters.Length) {
        n = a.Next(0, uppercaseLetters.Length); // 0-25   //upper
    }
    else if (temp_n < uppercaseLetters.Length * 2) {
        n = a.Next(0, uppercaseLetters.Length) + 100; // 100-125   //lower
    }
    else if (temp_n < uppercaseLetters.Length * 2 + symbols.Length) {
        n = a.Next(0, symbols.Length) + 200; // 200-220  //symbol
    }
    else if (temp_n < uppercaseLetters.Length * 2 + symbols.Length + numbers.Length) {
        n = a.Next(0, numbers.Length) + 300; // 300-309  // number
    }/*else if(temp_n < uppercaseLetters.Length * 2 + symbols.Length + numbers.Length + 1){ //newline ucun 1 geldik
        n = 400;
    }*/
    else {
        Console.ForegroundColor = ConsoleColor.Red;
        throw new Exception("Error: Index out of range in RandomUsing.");
    }

    if (global_key.Contains(n)) {
        return false; // prevent replicate
    } else {
        global_key.Add(n);
        temp_n++;
        return true;
    }
}


public static string Encrypt_Key()
{
     default_key = default_key_string.Split(',').Select(s => int.Parse(s.Trim())).ToList();
    Key_Decrypt.Decrypt_Key_Dictionary(default_key);
    List<int> temp_Liste = new List<int>();
    foreach(var a in global_key){
        temp_Liste.Add(a);
    }
   
    temp_Liste = Key_Decrypt.EncrypteList(temp_Liste);
    
    foreach(int a in temp_Liste){

            encrypted_key_array_string.Add(EncryptString(a.ToString(),1));        
    }

    return AESCipher.Encrypt(string.Join(',',encrypted_key_array_string));

}
 



public static string Encrypt_Key_Admin()
{
     default_key = default_key_string.Split(',').Select(s => int.Parse(s.Trim())).ToList();
    Key_Decrypt.Decrypt_Key_Dictionary(default_key);
    List<int> temp_Liste = new List<int>();
    foreach(var a in global_key){
        temp_Liste.Add(a);
    }
   Console.WriteLine("Key after :" + string.Join(',',temp_Liste));
    temp_Liste = Key_Decrypt.EncrypteList(temp_Liste);
    Console.WriteLine("Key before :" + string.Join(',',temp_Liste));
    foreach(int a in temp_Liste){

            encrypted_key_array_string.Add(EncryptString(a.ToString(),1));        
    }

    return string.Join(',',encrypted_key_array_string);

}
 



public static void EncryptValues()
{
    int totalNumber = uppercaseLetters.Length + lowercaseLetters.Length + symbols.Length + numbers.Length ; // 1 newline 
    
    Random randomGenerator = new Random();

    //
    if(global_key.ToArray().Length ==0){
    while (global_key.Count < totalNumber)
    {
        RandomUsing(randomGenerator);
    }
    }

    // UpperCase 
    for (int i = 0; i < uppercaseLetters.Length; i++) {
        toEncrypteDictionary[uppercaseLetters[i]] = encrypte_Upper[global_key[i]];
    }

    // LowerCase 
    for (int i = 0; i < lowercaseLetters.Length; i++) {
        toEncrypteDictionary[lowercaseLetters[i]] = encrypte_Lower[global_key[i + uppercaseLetters.Length] - 100];
    }

    // Symbol 
    for (int i = 0; i < symbols.Length; i++) {
        toEncrypteDictionary[symbols[i]] = encrypte_Symbols[global_key[i + uppercaseLetters.Length * 2] - 200];
    }

    // Number
    for (int i = 0; i < numbers.Length; i++) {
        toEncrypteDictionary[numbers[i]] = encrypte_Number[global_key[i + uppercaseLetters.Length * 2 + symbols.Length] - 300];
    }
}
    //newline 
    
     //   toEncrypteDictionary['\n'] =encrpyte_newline;
    
//}

   /* static async Task<string> Key_EncryptAsync()
    {


        using (Aes aes = Aes.Create())
        {
            aes.IV = iv;
            aes.Key = key;
            aes.KeySize = 128;
            aes.BlockSize = 128;  // AES standard block size
        aes.Padding = PaddingMode.PKCS7;  // Ensure padding mode matches encryption padding

            ICryptoTransform encrypted = aes.CreateEncryptor(aes.IV, aes.Key);

            using (MemoryStream mEncrypt = new MemoryStream())
            {
                using (CryptoStream crEncrypt = new CryptoStream(mEncrypt, encrypted, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(crEncrypt))
                    {


                        /* StringBuilder stBuilder = new StringBuilder();
                         *
                         *                         foreach(var num in global_key.ToArray())
                         *                         {
                         *                             stBuilder.Append(num);
                         *
                    }*//*
                       // string show_key = string.Join(",", global_key);
                        await writer.WriteAsync(Encrypt_Key());
                        await writer.FlushAsync();
                    }
                    bytes = mEncrypt.ToArray();
                }
            }
        }
        return Convert.ToBase64String(bytes);
    }  */

   public static string EncryptString( string plainText,int b = 0)
    {
       // int[] Key = global_key.ToArray();
       
       StringBuilder builder = new StringBuilder();
        if(b ==0){
        foreach (char c in plainText)
        {
            if(c == '\n'){
                builder.Append(encrpyte_newline);
            }else if(c == '"'){
                builder.Append(encrypte_dirnaq);
            }
            else if(toEncrypteDictionary.TryGetValue(c,out string encryptedValue)){
                builder.Append(encryptedValue);
            }else{
                builder.Append(c);
            }
        }
        }else{
            foreach (char c in plainText)
        {
            if(Key_Decrypt.decDictionary.TryGetValue(c,out string encryptedValue)){
                builder.Append(encryptedValue);
            }else{
                builder.Append(c);
            }
        }
        }
        return builder.ToString();

    }



   public static async Task EncryptFilesAndDirectoriesAsync(string directoryPath)
    {

        await FileEncryptAsync(directoryPath);


        await FileNameEncryptAsync(directoryPath);


        foreach (var subDirectory in Directory.GetDirectories(directoryPath))
        {
            await EncryptFilesAndDirectoriesAsync(subDirectory);
        }


        string directoryName = Path.GetFileName(directoryPath);
        string encryptedDirectoryName = EncryptString(directoryName);
        string parentDirectory = Path.GetDirectoryName(directoryPath);
        string newDirectoryPath = Path.Combine(parentDirectory, encryptedDirectoryName);


        if (!Directory.Exists(newDirectoryPath))
        {
            Directory.Move(directoryPath, newDirectoryPath);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Directory encrypted:"); 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{directoryPath}  ->  {newDirectoryPath}");
        }
        else
        {   Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Directory name collision: {newDirectoryPath} already exists. Skipping.");
        }
    }

    public static async Task FileNameEncryptAsync(string directoryPath)
    {
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            string fileName = Path.GetFileName(file);
            string encryptedFileName = EncryptString( fileName);
            string newFilePath = Path.Combine(directoryPath, encryptedFileName);


            if (!File.Exists(newFilePath))
            {
                File.Move(file, newFilePath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("File encrypted: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{file}  ->  {newFilePath}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"File name collision: {newFilePath} already exists. Skipping.");
            }
        }
    }

   public  static async Task FileEncryptAsync(string directoryPath)
    {
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            byte[] fileBytes =  File.ReadAllBytes(file);
            string encryptedFileContent = EncryptString(Encoding.UTF8.GetString(fileBytes));

             File.WriteAllText(file, encryptedFileContent);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("File content encrypted: ");
            Console.ForegroundColor = ConsoleColor.Blue;

           Console.WriteLine($"{file}");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }


public static async Task EncryptedFIlePath(string file){
    
    
        byte[] bytes =  File.ReadAllBytes(file);
        string encryptedFileContexs = EncryptString(Encoding.UTF8.GetString(bytes));
         File.WriteAllText(file,encryptedFileContexs);
         Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("File content encrypted:");
        Console.ForegroundColor = ConsoleColor.Blue;
         Console.WriteLine(file);
         Console.ForegroundColor = ConsoleColor.Cyan;
        string fileName = Path.GetFileName(file);
        string encryptedFileName = EncryptString(fileName);
        string directoryPath = Path.GetDirectoryName(file);
        string newFilePath = Path.Combine(directoryPath,encryptedFileName);
        if (!File.Exists(newFilePath))
            {
                File.Move(file, newFilePath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"File encrypted:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{file}  ->  {newFilePath}");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
               
                Console.WriteLine($"File name collision: {newFilePath} already exists. Skipping.");
               
                Console.ForegroundColor = ConsoleColor.Cyan;

            }
}


public static bool encrpyte_Prosess = true;

public static bool Win32Mode = false;
public static bool Private_Mode = false;
public static void Main_EnT(string[] args){



    if(args.Contains("-P")){
        Private_Mode = true;
    }

    if(args.Length==0){
        throw new Exception("Error:argumant is empty!");
        
    
    }else if(args[0] == "--admin"){
        if(args[1] == "SaveWorld"){
            Mains().GetAwaiter().GetResult();
        }
    }
    else if(args.Contains("-d") && args.Contains("-e")){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error : two opposite argumans!");
        Console.ResetColor();
    }
    else if(args.Contains("--help") || args.Contains("-h")){
        Argumans.DisplayHelp();
    }
    
    else if (args.Contains("--Win32")){
        Win32Mode =true;
        int arg_num =0;
        
       
        
        foreach(var arg in args){

            switch (arg) {

                case "-K" :
                
                    Argumans.Key_Set(args[arg_num+1],"Win32");
                    break;
                
                case "-P" :
                    Private_Mode = true;
                    break;
                case "-d" :
                    encrpyte_Prosess = false;
                    break;
                case "-e" :
                    encrpyte_Prosess = true;
                    break;
                case "--txt" :
                    Argumans.ChooseType(args[arg_num+1]);
                    break;
                
                case "-D" :
                    Argumans.ChooseType(args[arg_num+1],1);
                    break;
                
                case "-f" :
                    Argumans.ChooseType(args[arg_num+1],2);
                    break;
                case "-h" :
                    Argumans.DisplayHelp();
                    break;
                case "--help" :
                    Argumans.DisplayHelp();
                    break;
            }
            arg_num++;
        }
       if(!args.Contains("--txt") && !args.Contains("-D") && !args.Contains("-f")) {
           Argumans.ChooseType(args[args.Length-1]);
           
       }

    }
    else{
        int arg_num =0;
        foreach(var arg in args){
            switch(arg){
                case "-P" :
                    Private_Mode = true;
                    break;
                case "-K" :
                    Argumans.Key_Set(args[arg_num+1]);
                    break;
                case "-d" :
                    encrpyte_Prosess = false;
                    break;
                case "-e" :
                    encrpyte_Prosess = true;
                    break;
                case "--txt" :
                    Argumans.ChooseType(args[arg_num+1]);
                    break;
                
                case "-D" :
                    Argumans.ChooseType(args[arg_num+1],1);
                    break;
                
                case "-f" :
                    Argumans.ChooseType(args[arg_num+1],2);
                    break;
                case "-h" :
                    Argumans.DisplayHelp();
                    break;
                case "--help" :
                    Argumans.DisplayHelp();
                    break;
            }
            arg_num++;
        }
    }
   Console.ResetColor();
}
    public static async Task Mains()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("File Encrypt? [y/n] default [n] :");
        Console.ForegroundColor = ConsoleColor.Magenta;
        string yesno = Console.ReadLine();

        if (yesno == "y")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Pls enter Path: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string fPath = Console.ReadLine().Trim();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string filePath = @fPath;

            if (Directory.Exists(filePath))
            {
                Echarcters.Choose_Echarcters("Linux");
                EncryptValues();
                await EncryptFilesAndDirectoriesAsync(filePath);


                Console.ForegroundColor = ConsoleColor.Cyan;
              //  Console.WriteLine($"Encryption Key:{Console.ForegroundColor = ConsoleColor.Magenta} {await Key_EncryptAsync()}");
              

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Encrypt_Key());

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(string.Join(",", global_key));
                Console.ForegroundColor = ConsoleColor.Cyan;


                 } else if(File.Exists(filePath)){
                Echarcters.Choose_Echarcters("Linux");
                EncryptValues();
                await   EncryptedFIlePath(filePath);
               // Console.WriteLine($"Encryption Key: {Console.ForegroundColor = ConsoleColor.Magenta}{await Key_EncryptAsync()}");
               Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Encrypt_Key());

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(string.Join(",", global_key));
                Console.ForegroundColor = ConsoleColor.Cyan;

                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid directory path.");
                Console.ResetColor();
            }
        }else if(yesno == "x"){
            Echarcters.Choose_Echarcters("Linux");
            EncryptValues();
           
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(string.Join(",", global_key));
                Console.ForegroundColor = ConsoleColor.Cyan;

           
        }
        
        else if(yesno == "s") {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Text [y/n] :");
        Console.ForegroundColor = ConsoleColor.Magenta;
    string inputv = Console.ReadLine().Trim();
    Console.ForegroundColor = ConsoleColor.Cyan;
    if(inputv == "y"){
        Console.ForegroundColor = ConsoleColor.Cyan;
         Console.Write("Pls enteer text : ");try{
            Console.ForegroundColor = ConsoleColor.Magenta;
    string input_text = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
        Echarcters.Choose_Echarcters("Linux");
         EncryptValues();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Cipher Text :");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Encrypte.EncryptString(input_text));
       
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Encryption Key: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Encrypt_Key());


            Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(string.Join(",", global_key));
                 Console.ForegroundColor = ConsoleColor.Cyan;

        }catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error:{e.Message}");
            Console.ResetColor();
        }
    }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("File Decrypt? [y/n] default [n]:");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string decryptChoice = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (decryptChoice == "y")
            {
              await Decrypte.Main_Decrypt();


            }
            else
            {
                Environment.Exit(2);
            }
        }
    }
}

