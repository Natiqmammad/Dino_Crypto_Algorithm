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

public class Echarcters{

    public static string[] encrypte_Lower = {
        "q+s!a", "q+d@a", "q!+af", "Aq+gS", "+fd!a", "w=+_j", "e@d+d", "eh3+q", "e+08r", "e!4tf", "e!w5y", "5=qaa",
        "1_q5d", "+q=wf", "_=qh2", "=sa+s", "q=hKf", "wF=Zq", "eQ=o!", "_@qad", "!q!sg", "==dJs", "@p@o@",
        "@i_w=", "!=b~o", "~!!~p", "~#k=k", "~K~qw", "~sKx@", "@~t#5", "~#w=q", "~vK=R", "H!J~a","k_9x1" ,"JFd_8" ,"34Fsa" ,"Sa03F" ,"Ba.S_",".SAm_" ,"LaAm_"
    };
    public static string[] encrypte_Upper = {
        "q~S!a", "q~D@A", "q!~AF", "qA~Gs", "~f!Da", "w~@J=", "e@d~D", "eH2~Q", "e2~R3", "e~T!=", "e@~Yd", "~=qAa",
        "~q~DQ", "S~q~F", "S~q~H", "~KsAs", "q#~H=", "W@w~Q", "O!e~O", "~@qAd", "~q~SL", "~=dSM", "#pB~E",
        "~Gv_W", "V~b~O", "~!V~n", "~#Kn=", "~!a=L", "~sC=X", "tx_S!", "~#w=Q", "~V_v@", "~A=vb" , "~kXx@" , "k_@x1" ,"J=d_8" ,"3=Fsa" ,"sA05F" ,"BA.S_","#sAM_" ,"HaAm#"
    };
    public static string[] encrypte_Number ={"k(~t~",
    "k~!f~",
    "g_)@F",
    "l=lk~",
    "!~f=!",
    "xP%dg",
    "m~#t~",
    "d!$n@",
    "p#F)!",
    "z^r@!"};

private static string[] encrypte_Symbols = {
    "r!~ks", "r!s~f", "r!s~d", "dC~!a", "a_1~d", "g~=f!", "~_q=w", "lk=~d", "sv~d_", "d~~fd",
    "~a_2d", "d!_~g", "fO~r1", "u_5@n","uB#_n" ,"B_@7n", "mJ~k2", "zxXzR", "XXDas", "xN_Nx" , "NxN_X", "n&=~b", "c&!~x", "l&!~m", "p&~=d", "qT~d3", "j~l1Y",
    "xL~d!", "vR!~c", "bC~q2", "zX~r!"
};
  public  static void Choose_Echarcters(string osName){
        if(osName == "Win32")
        {
            Compare_Echarcters();
        } 
    }

    static void Compare_Echarcters(){
        Encrypte.encrypte_Lower = encrypte_Lower;
        Encrypte.encrypte_Number = encrypte_Number;
        Encrypte.encrypte_Symbols = encrypte_Symbols;
        Encrypte.encrypte_Upper = encrypte_Upper;
    }
}

