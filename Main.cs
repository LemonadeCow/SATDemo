using System;
using System.Collections.Generic;

/**
  * Hitbox Program
  *
  * This is the Csharp version of my Javascript program where I tried
  * to develop a different hitbox that is based on the Seperating Axis
  * Theorem.
  *
  * sources:
  *
  *
  */

namespace HitBox
{

  public class MathThings{}
  public static class MathExtension
  {
    public static double VectorDotProduct(this MathThings mc, Vector v1, Vector v2)
    {
      return((v1.X*v2.X) + (v1.Y*v2.Y));
    }

    public static double CalcMagnitude(this MathThings mc, Vector v)
    {
      v.magnitude = Math.Sqrt(Math.Pow(v.X, 2)) + (Math.Pow(v.X, 2));
      return (v.magnitude);
    }
  }
  //This class will be used to detect collision
  public class Check
  {
    //Temporary method
    public static void Main(string [] args)
    {

      MathThings MathThings = new MathThings();
      //Polygon rectangle = new Polygon(1,4,2,4,2,3,1,1);
        //14242311

      Vector p1 = new Vector(1, 4);
      Vector p2 = new Vector(2, 4);
      Vector p3 = new Vector(2, 3);
      Vector p4 = new Vector(1, 3);

      Polygon Rectangle = new Polygon(p1, p2, p3, p4);

      Console.Write(MathThings.CalcMagnitude(Rectangle.Vertices[0]));


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

    public double magnitude {get; set;}

    public Vector(double x, double y)
    {
      this.X = x;
      this.Y = y;

      this.magnitude = 0;

      Coords = new double [] {X, Y};

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
   * my code and label my "collection" of vectors
   *
   */

  public class Polygon
  {

    //public Dictionary<string, int[]> verticesDict =  new Dictionary<string, int[]>();
    public Vector [] Vertices {get; set;}

    public double IniMax {get; set;}
    public double IniMin {get; set;}

    PerpendicularAxis PerpAxis {get; set;}

    public Polygon(params Vector[] vertices)
    {
      Vertices = vertices;

      PerpAxis = new PerpendicularAxis(Vertices[0]);

      /*for(int i = 0; i < Vertices.Length; i++)
      {
        verticesDict.Add("v" + (i+1), Vertices[i].ReadCoords());
      }*/

    }

    public class PerpendicularAxis
    {

      public Vector Vector {get; set;}

      public PerpendicularAxis(Vector v)
      {
        Vector = v;
      }
    }
  }
}
