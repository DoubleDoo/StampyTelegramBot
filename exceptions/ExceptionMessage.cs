using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;

public static class ExceptionMessage
{
    static ExceptionMessage() { }

    public static void ExceptionString(Exception ex, string msg)
    {
        StackTrace st = new StackTrace(ex, true);
        StackFrame frame = st.GetFrame(st.FrameCount - 1);
        Console.WriteLine("\nException:\n" +
               ex.GetType().ToString() + "\n" +
               $"In file {frame.GetFileName()} at row {frame.GetFileLineNumber()} in method {frame.GetMethod()}\n" +
               msg + "\n\n");
    }

    public static void UnknownExceptionString(Exception ex)
    {
        Console.WriteLine($"\nUnhandled Exception:\n{ex.GetType().ToString()}\n" +
               $"{ ex.Message}\n\n");
    }
    public static void ExceptionString(Exception ex,string str,string msg)
    {
        StackTrace st = new StackTrace(ex, true);
        StackFrame frame = st.GetFrame(st.FrameCount - 1);
        Console.WriteLine("\nException:\n" +
               ex.GetType().ToString()+"\n"+
               $"In file {frame.GetFileName()} at row {frame.GetFileLineNumber()} in method {frame.GetMethod()}\n" +
               msg+"\n"+
               $"Argument [{str}]\n\n");
    }

    public static void UnknownExceptionString(Exception ex, string str)
    {
        Console.WriteLine($"\nUnhandled Exception:\n{ex.GetType().ToString()}\n" +
               $"{ ex.Message}\n" +
               $"Argument [{str}]\n\n");
    }
}

   




