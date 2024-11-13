using System.ComponentModel;
using Microsoft.AspNetCore.SignalR;
public class Triangle
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public double CalculatePerimeter()
    {
        return SideA + SideB + SideC;
    }

    public double CalculateArea()
    {
        double semiPerimeter = CalculatePerimeter() / 2;
        return Math.Sqrt(semiPerimeter * (semiPerimeter - SideA) * (semiPerimeter - SideB) * (semiPerimeter - SideC));
    }

    public (double x, double y) CalculateCentroid()
    {
         // For simplicity, assuming equilateral triangle coordinates as an example.
        // In practice, the centroid calculation would depend on actual coordinates.
        return (0, 0);  // Adjust based on real triangle coordinates.
    }

    public bool IsValid()
    {
        return SideA + SideB > SideC && SideA + SideC > SideB && SideB + SideC > SideA;
    }
}