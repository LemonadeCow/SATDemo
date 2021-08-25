/**
  * Questions:
  *
  * Is the PerpendicularAxis the same as a normal on a vector?
  *
  * YES
  *
  * What exactly am I doing when I'm normalizing a vector?
  *
  * I am changing the Vector's Magnitude to 1 ?
  *
  * What is a unit vector? Is it just a normalized vector?
  *
  * Yes?
  *
  * How can I project the min/max values of the polygon on an axis?
  * This question also feeds into loops.
  *
  * Using the VectorDotProduct gives me the projection
  *
  * Notes:
  *
  * Vectors are cool
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
  *
  * https://www.sevenson.com.au/programming/sat/
  * https://www.metanetsoftware.com/technique/tutorialA.html
  * https://gamedevelopment.tutsplus.com/tutorials/collision-detection-using-the-separating-axis-theorem--gamedev-169
  * https://dyn4j.org/2010/01/sat/
  *
  * Things to Try:
  * ArrayLists
  * Make a graphical representation
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

      Polygon Square = new Polygon(p1, p2, p3, p4);

      Vector pp1 = new Vector(1, 4);
      Vector pp2 = new Vector(2, 4);
      Vector pp3 = new Vector(2, 3);

      Polygon Triangle = new Polygon(pp1, pp2, pp3);

      CheckPolygons(Triangle, Square);

      //MathThings.VectorDotProduct(p1, p2);
    }

    public static void CheckPolygons(Polygon p, Polygon p2)
    {

      MathThings MathThings = new MathThings();

      for(int a = 0; a < p.Vertices.Length; a++)
      {

        p.PerpAxis.getPerpAxis(p, a);
        MathThings.NormalizeVector(p.PerpAxis.Vector);

        p.IniMax = MathThings.VectorDotProduct(p.PerpAxis.Vector, p.Vertices[0]);
        p.IniMin = p.IniMax;

        p2.IniMax = MathThings.VectorDotProduct(p.PerpAxis.Vector, p2.Vertices[0]);
        p2.IniMin = p2.IniMax;

        for(int i = 0; i < p.Vertices.Length; i++)
        {

          if(p.IniMax < MathThings.VectorDotProduct(p.PerpAxis.Vector, p.Vertices[i]))
          {
            p.IniMax = MathThings.VectorDotProduct(p.PerpAxis.Vector, p.Vertices[i]);
          }

          if(p.IniMin > MathThings.VectorDotProduct(p.PerpAxis.Vector, p.Vertices[i]))
          {
            p.IniMin = MathThings.VectorDotProduct(p.PerpAxis.Vector, p.Vertices[i]);
          }

        }

        for(int i = 0; i < p2.Vertices.Length; i++)
        {

          if(p2.IniMax < MathThings.VectorDotProduct(p.PerpAxis.Vector, p2.Vertices[i]))
          {
            p2.IniMax = MathThings.VectorDotProduct(p.PerpAxis.Vector, p2.Vertices[i]);
          }

          if(p2.IniMin > MathThings.VectorDotProduct(p.PerpAxis.Vector, p2.Vertices[i]))
          {
            p2.IniMin = MathThings.VectorDotProduct(p.PerpAxis.Vector, p2.Vertices[i]);
          }

        }
        Console.WriteLine("p1 IniMax = " + p.IniMax);
        Console.WriteLine("p1 IniMin = " + p.IniMin);

        Console.WriteLine("p2 IniMax = " + p2.IniMax);
        Console.WriteLine("p2 IniMin = " + p2.IniMin);

        if(p.IniMax >= p2.IniMax || p.IniMin >= p2.IniMin)
          Console.WriteLine("Both Polygons Are Touching On This Axis");
        else
          Console.WriteLine("Both Polygons Are Not Touching On This Axis");

        if(a < p.Vertices.Length - 1)
          Console.WriteLine("Checking The Next Axis");

      }

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

  public class Vector
  {

    public double [] Coords {get; set;}

    public double X {get; set;}
    public double Y {get; set;}

    public double Magnitude {get; set;}

    //Vector constructor
    public Vector(double X, double Y)
    {

      this.X = X;
      this.Y = Y;
      this.Coords = new double [] {X, Y};

      this.Magnitude = Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));

      Console.WriteLine("Vector's Magnitude = " + this.Magnitude);
    }

    public double[] ReadCoords()
    {
      return (this.Coords); // used this to debug
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

    public Vector [] Vertices {get; set;}

    public double IniMax {get; set;}
    public double IniMin {get; set;}

    public Vector RectDot {get; set;}

    public PerpendicularAxis PerpAxis {get; set;}

    //Polygon constructor
    public Polygon(params Vector[] Vertices)
    {
      this.IniMin = 0;
      this.IniMax = 0;

      this.Vertices = Vertices;

      this.PerpAxis = new PerpendicularAxis();

    }

    /**
      * The PerpendicularAxis Class !!!!
      *
      * This class is neseted inside of the Polygon Class and is used
      * as a way to find and store the PerpendicularAxis of the Polygon
      *
      */

    public class PerpendicularAxis
    {

      public Vector Vector {get; set;}

      public PerpendicularAxis()
      {
        this.Vector = new Vector(0, 0);
        //Initializing the Variable

        Console.WriteLine("PerpAxis = 0");
        //debugging...

      }

      //This method gets the PerpAxis relative to the index
      public void getPerpAxis(Polygon p, int i)
      {
        if(i == (p.Vertices.Length - 1))
        {
          this.Vector.X = -(p.Vertices[i].X - p.Vertices[0].X);
          this.Vector.Y = p.Vertices[i].Y - p.Vertices[0].Y;
          //Finds the Vector's normal but also connects the last point with the 1st point
        }
        else
        {
          this.Vector.X = -(p.Vertices[i].X - p.Vertices[i + 1].X);
          this.Vector.Y = p.Vertices[i].Y - p.Vertices[i + 1].Y; //i + 1 doesn't seem right
          //Finds the Vector's normal
        }
      }
    }
  }
}
