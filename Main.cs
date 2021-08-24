/**
  * Questions:
  *
  * Is the PerpendicularAxis the same as a normal on a vector?
  *
  * What exactly am I doing when I'm normalizing a vector?
  *
  * What is a unit vector? Is it just a normalized vector?
  *
  * How can I project the min/max values of the polygon on an axis?
  * This question also feeds into loops.
  *
  *
  * Notes:
  *
  *
  *
  *
  */

using System;
using System.Collections.Generic;

/**
  * Collision Detection Program !
  *
  * This is the Csharp version of my Javascript program where I tried
  * to develop a different Collision Detection Algorithim that is based
  * on the Seperating Axis Theorem.
  *
  * sources:
  * https://www.sevenson.com.au/programming/sat/
  * https://www.metanetsoftware.com/technique/tutorialA.html
  * https://gamedevelopment.tutsplus.com/tutorials/collision-detection-using-the-separating-axis-theorem--gamedev-169
  * https://dyn4j.org/2010/01/sat/
  *
  * Things to Try:
  * ArrayLists
  *
  */

namespace CollisionDetection
{
  using VectorMath;
  //Imports the VectorMath namespace

  //This class will be used to check collisions (but is also being used)
  //to just setup the program
  public class Check
  {

    //Temporary Main Method
    public static void Main(string [] args)
    {

      MathThings MathThings = new MathThings();

      Vector p1 = new Vector(1, 4);

      Vector p2 = new Vector(2, 4);
      Vector p3 = new Vector(2, 3);
      Vector p4 = new Vector(1, 3);

      Polygon Rectangle = new Polygon(p1, p2, p3, p4);

      //Rectangle.PerpAxis.getPerpAxis(Rectangle);

      MathThings.CalculateMagnitude(p1);
      MathThings.NormalizeVector(p1);
      MathThings.VectorDotProduct(p1, p2);
    }

    public static void SetPoints(Polygon p)
    {
      MathThings MathThings = new MathThings();

      p.IniMin = MathThings.VectorDotProduct(p.PerpAxis.Vector, p.Vertices[0]); // 0 will become an index when I actually loop it
      p.IniMax = p.IniMin;

      Console.WriteLine(p.IniMin);
      Console.WriteLine(p.IniMax);

      for (int i = 0; i < p.Vertices.Length; i++)
      {
        /*
        this.polygon[i].rectDot = polygon[i].x;
        this.polygon[i].iniMin = Math.min(this.polygon[i].iniMin , this.polygon[i].rectDot);
        this.polygon[i].iniMax = Math.max(this.polygon[i].iniMax , this.polygon[i].rectDot);

        //this is my placeholder code until I start looping the variables
      */
      }

    }

    public static void ProjectPoints(Polygon p)
    {

    }

  }

  /**
    * The VectorMath namespace !
    *
    * This is just a package to store all my VectorMath functions
    *
    */

  namespace VectorMath
  {

    public class MathThings{}

    public static class MathExtension
    {

      public static double VectorDotProduct(this MathThings mc, Vector v1, Vector v2)
      {
        return((v1.X*v2.X) + (v1.Y*v2.Y));
      }

      public static double CalculateMagnitude(this MathThings mc, Vector v)
      {
        v.Magnitude = Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));

        Console.WriteLine("Vector's Magnitude = " + v.Magnitude);

        return (v.Magnitude);
      }

      public static void NormalizeVector(this MathThings mc, Vector v)
      {
        if(v.Magnitude != 0)
        {

          v.X /= v.Magnitude;
          v.Y /= v.Magnitude;

          Console.WriteLine("Normalized Vector.X = " + v.X);
          Console.WriteLine("Normalized Vector.Y = " + v.Y);
        }
        else
        {
          v.X = v.X;
          v.Y = v.Y;
        }
      }

    }

  }

/**
  *
  * The Vector Class !
  *
  * Used as a "data type" that stores 2
  * or more points. I say 2 or more since I might reuse some of this code
  * if I ever decide to make a 3D rendition of this project.
  *
  */

  /*

  // using PerpendicularAxis;

  I might need to imprort this if I want to create a PerpAxis
  method inside of the vector class

  */

  public class Vector
  {

    public double [] Coords {get; set;}

    public double X {get; set;}
    public double Y {get; set;}

    public double Magnitude {get; set;}

    /*
    public PerpendicularAxis PerpAxis;
    This throws CS0246 : The type or namespace name 'PerpendicularAxis' could not be found
    */

    //Vector constructor
    public Vector(double X, double Y)
    {
      this.X = X;
      this.Y = Y;

      this.Magnitude = 0;

      this.Coords = new double [] {X, Y};
    }

    public double[] ReadCoords()
    {
      return (Coords);
    }

  }

 /**
   *
   * The Polygon Class !
   *
   * Used as a way to store 3 or more vertices in order to help organize
   * my code and label my "array" of vectors
   *
   * Also used as a way to store various methods and objects
   *
   */

  public class Polygon
  {

    //public Dictionary<string, int[]> verticesDict =  new Dictionary<string, int[]>();

    public Vector [] Vertices {get; set;}

    public double IniMax {get; set;}
    public double IniMin {get; set;}

    public Vector RectDot {get; set;}

    public PerpendicularAxis PerpAxis {get; set;}

    //Polygon constructor
    public Polygon(params Vector[] Vertices)
    {

      this.Vertices = Vertices;

      for(int i = 0; i < Vertices.Length; i++)
      {
        /*
        this.Vertices[i].PerpAxis = new PerpendicularAxis();

        ? does this work in theory?????????????????
        */
      }

      /*
      for(int i = 0; i < Vertices.Length; i++)
      {
        verticesDict.Add("v" + (i+1), Vertices[i].ReadCoords());
      }
      */

    }

    public class PerpendicularAxis
    {

      public Vector Vector {get; set;}
      public Polygon Polygon {get; set;}

      public PerpendicularAxis()
      {
        this.Vector = new Vector(0,0);
      }

      public void getPerpAxis(Polygon p)
      {
        this.Polygon = p;

        this.Vector.X = -(this.Polygon.Vertices[0].X - this.Polygon.Vertices[0+1].X); //Will use a loop later
        this.Vector.Y = this.Polygon.Vertices[0].Y - this.Polygon.Vertices[0+1].Y; //Will use a loop later
      }
    }
  }
}
