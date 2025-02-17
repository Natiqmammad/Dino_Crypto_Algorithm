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

public class Argumans{
public static string[] arguman = {"--Win32","-K","-D","-d","-e","--txt","-f","-R","-h","--help","-P"};

public static Dictionary<string,string> argDic = new Dictionary<string, string>();

private static void DicAdd(){
    argDic["--Win32"] = "--Encrypt Format";
    argDic["--txt"] = "--Use for Text";
    argDic["--help"] = "--Help [Available commands]";
    argDic["--aes"] = "--Result encrypt or decrypt with aes";
    argDic["-K"] = "--Set Key";
    argDic["-D"] = "--Use for Dictionary";
    argDic["-f"] = "--Use for file";

    argDic["-d"] = "--Decrypt Prosess";
    argDic["-e"] = "--Encrypt Prosess";
    argDic["-P"] = "--Private Mode";
    argDic["-h"] = "--Help [Available commands]";
    argDic["-R"] = "--Use for encrypt/decrypt file and folder Rename";
    
    
   
}

public static void DisplayHelp()
    {
        DicAdd();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("         \tAvailable Commands:     ");
        foreach (var arg in argDic)
        {   
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{arg.Key}:      \t\t");
             Console.ForegroundColor = ConsoleColor.Green;
             Console.WriteLine($"{arg.Value}");
             Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }

    public static void Key_Set(string decryptedKey,string osName ="Linux"){
       
       
    decryptedKey = AESCipher.Decrypt(decryptedKey);
    string[] keyParts = decryptedKey.Split(',');


    Encrypte.default_key = Encrypte.default_key_string.Split(',').Select(s => int.Parse(s.Trim())).ToList();
    
    Echarcters.Choose_Echarcters(osName);

 Decrypte.DecrypteReverseDictionary(Encrypte.default_key,Decrypte.decryptDictionary);
    
    foreach(var part in keyParts){
       Encrypte.global_key.Add(int.Parse(Decrypte.DecryptedString(part,1)));
    }
    
    
    Encrypte.global_key = Key_Decrypt.DecrypteList(Encrypte.global_key);

    

    if(!Encrypte.Private_Mode){

    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Encrypte.Encrypt_Key());
    }

    }


    
    public static void ChooseType(string pathOrTxt,int number=0){
        if (Encrypte.Win32Mode){
                    Echarcters.Choose_Echarcters("Win32");
            }
        
        if(number ==0){
            
            if(Encrypte.encrpyte_Prosess){
                
                    Encrypte.EncryptValues();
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"string plaintext : {pathOrTxt}");
                Console.ForegroundColor = ConsoleColor.Cyan;
               Console.Write("CipherText : ");
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine (Encrypte.EncryptString(pathOrTxt));
                if(!Encrypte.Private_Mode){
                   
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Encrypte.Encrypt_Key());
                Console.ForegroundColor = ConsoleColor.Cyan;

                }
               }else{
                
               Console.ForegroundColor = ConsoleColor.Cyan;
               Console.Write("Plain Text : ");
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine(Decrypte.DecryptedString(pathOrTxt));
               /*if(!Encrypte.Private_Mode){

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Encryption Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Encrypte.Encrypt_Key());


                }*/
            }
            
        }else if(number ==1){
            
            if(!Directory.Exists(pathOrTxt)){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This Directory:{pathOrTxt} not exist!");
                throw new Exception($"This Directory:{pathOrTxt} not exist!");
                return;
            }
            if(Encrypte.encrpyte_Prosess){

                Encrypte.EncryptValues();
                 if(Encrypte.Private_Mode){
                    Private_EncryptFilesAndDirectories(pathOrTxt);
                  }
                  else{
                    Encrypte.EncryptFilesAndDirectoriesAsync(pathOrTxt).GetAwaiter().GetResult();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Encryption Key: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Encrypte.Encrypt_Key());

                    }
            }else{

                if(Encrypte.Private_Mode){
                    Private_DecrypteFilesAndDirectories(pathOrTxt);
                  }
                  else{

                    try{
         Decrypte.DecrypteFilesAndDirectoriesAsync(pathOrTxt).GetAwaiter().GetResult();
             Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Encryption Key: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Encrypte.Encrypt_Key());
        }catch (Exception e){
            Console.ForegroundColor  = ConsoleColor.Red;
            Console.WriteLine($"Error:{e.Message}");
            Console.ResetColor();
        }

                  }
            }
        }else if(number ==2){

            if(!File.Exists(pathOrTxt)){
                 Console.ForegroundColor  = ConsoleColor.Red;
                Console.WriteLine($"This File:{pathOrTxt} not exist!");
                throw new Exception($"This File:{pathOrTxt} not exist!");
                return;
            }
            
            if(Encrypte.encrpyte_Prosess){ 

                Encrypte.EncryptValues();

                if(Encrypte.Private_Mode){
                    
                        Private_EncryptedFIlePath(pathOrTxt);
                  }
                  else{
                    Encrypte.EncryptedFIlePath(pathOrTxt).GetAwaiter().GetResult();
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Encryption Key: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Encrypte.Encrypt_Key());

                  }

            }
            else{
                
                if(Encrypte.Private_Mode){
             Private_FileDecrypt(pathOrTxt);
             Private_FileNameDecrypt(pathOrTxt);
                  }
                  else{

                    try{
                         Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Encryption Key: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Encrypte.Encrypt_Key());
         Decrypte.FileDecryptAsync(pathOrTxt).GetAwaiter().GetResult();
     
        Decrypte.FileNameDecryptAsync(pathOrTxt).GetAwaiter().GetResult();
     
        }catch (Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error:{e.Message}");
            Console.ResetColor();
        }

                  }
            }
        }
    }



static void Private_FileEncrypt(string directoryPath)
    {
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            byte[] fileBytes =  File.ReadAllBytes(file);
            string encryptedFileContent = Encrypte.EncryptString(Encoding.UTF8.GetString(fileBytes));

             File.WriteAllText(file, encryptedFileContent);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }

 private static void Private_FileNameEncrypt(string directoryPath)
    {
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            string fileName = Path.GetFileName(file);
            string encryptedFileName = Encrypte.EncryptString( fileName);
            string newFilePath = Path.Combine(directoryPath, encryptedFileName);


            if (!File.Exists(newFilePath))
            {
                File.Move(file, newFilePath);
                }
        }
    }

    private static void Private_EncryptFilesAndDirectories(string directoryPath)
    {

        Private_FileEncrypt(directoryPath);


        Private_FileNameEncrypt(directoryPath);


        foreach (var subDirectory in Directory.GetDirectories(directoryPath))
        {
           Private_EncryptFilesAndDirectories(subDirectory);
        }


        string directoryName = Path.GetFileName(directoryPath);
        string encryptedDirectoryName = Encrypte.EncryptString(directoryName);
        string parentDirectory = Path.GetDirectoryName(directoryPath);
        string newDirectoryPath = Path.Combine(parentDirectory, encryptedDirectoryName);


        if (!Directory.Exists(newDirectoryPath))
        {
            Directory.Move(directoryPath, newDirectoryPath);
            }
    }



public static void Private_EncryptedFIlePath(string file){
    
    
        byte[] bytes =  File.ReadAllBytes(file);
        string encryptedFileContexs = Encrypte.EncryptString(Encoding.UTF8.GetString(bytes));
         File.WriteAllText(file,encryptedFileContexs);
         
        
        string fileName = Path.GetFileName(file);
        string encryptedFileName = Encrypte.EncryptString(fileName);
        string directoryPath = Path.GetDirectoryName(file);
        string newFilePath = Path.Combine(directoryPath,encryptedFileName);
        if (!File.Exists(newFilePath))
            {
                File.Move(file, newFilePath);
            }
           
}



public static void Private_DecrypteFilesAndDirectories(string directoryPath){
    foreach (var item in Directory.GetFiles(directoryPath))
    {
       
         Private_FileDecrypt(item);
         Private_FileNameDecrypt(item);
    }

    foreach(var item in Directory.GetDirectories(directoryPath)){
       
       
        Private_DecrypteFilesAndDirectories(item);
    }

    string directoryName = Path.GetFileName(directoryPath);
        string decryptedDirectoryName = Decrypte.DecryptedString(directoryName);
        string parentDirectory = Path.GetDirectoryName(directoryPath);
        string newDirectoryPath = Path.Combine(parentDirectory, decryptedDirectoryName);

        if (!Directory.Exists(newDirectoryPath))
        {
            Directory.Move(directoryPath, newDirectoryPath);
             }
        

}

public static void Private_FileDecrypt(string directoryPath)
    {
        
            byte[] fileByte = File.ReadAllBytes(directoryPath);
            
            string decryptedFileContext = Decrypte.DecryptedString(Encoding.UTF8.GetString(fileByte).Trim());
            
            File.WriteAllText(directoryPath, decryptedFileContext);
            

    }




public static void Private_FileNameDecrypt(string directoryPath)
    {
        if(Directory.Exists(directoryPath)){
        foreach (var file in Directory.GetFiles(directoryPath))
        {
            Private_FileNameDecrypt(file);
        }
        foreach(var item in Directory.GetDirectories(directoryPath)){
        
         Private_FileNameDecrypt(item);
    }
    }
    ////
    else if(File.Exists(directoryPath)){

        string fileName = Path.GetFileName(directoryPath);
        
        string decryptedFileName = Decrypte.DecryptedString(fileName);
        string directoryPathH = Path.GetDirectoryName(directoryPath);
        string newFilePath = Path.Combine(directoryPathH,decryptedFileName);
        if (!File.Exists(newFilePath))
            {
                File.Move(directoryPath, newFilePath);
                  }
            

        
    }
   




}


}
