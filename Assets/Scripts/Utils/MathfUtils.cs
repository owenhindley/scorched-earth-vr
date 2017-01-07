using UnityEngine;
using System.Collections;

public static  class MathfUtils 
{
    public enum AngleMode
    {
        Radians,
        Degrees,
    }

    public static float ShortAngle(float angle)
    {
        return ShortAngle(angle, AngleMode.Degrees);
    }

    /// <summary>
    /// Convert to the short angle
    /// Taken from https://github.com/greensock/GreenSock-JS/blob/master/src/uncompressed/plugins/DirectionalRotationPlugin.js
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="mode">Degress or Radians</param>
    /// <returns></returns>
    public static float ShortAngle(float angle, AngleMode mode)
    {
        var limit = (mode == AngleMode.Radians) ? Mathf.PI * 2f : 360;
        var a = angle % limit;
        if(a != a % (limit/2f))
        {
            a = (a <= 0) ? a + limit : a - limit;
        }
        return a;
    }

    public static Vector3 ShortAngle(Vector3 angle)
    {
        return ShortAngle(angle, AngleMode.Degrees);
    }

    public static Vector3 ShortAngle(Vector3 angle, AngleMode mode)
    {
        return new Vector3(
                ShortAngle(angle.x, mode),
                ShortAngle(angle.y, mode),
                ShortAngle(angle.z, mode)
               );
    }


    public static float LinearStep(float left, float right, float x)
    {
        return Mathf.Clamp01((x - left) / (right - left));
    }

    public static float FBM(Vector3 p, int octaves)
    {
        float total = 0.0f;
        float scale = 1.0f;

        for (int i = 0; i < octaves; ++i)
        {
            total += Simplex.Noise.Generate(p.x, p.y, p.z) * scale;

            scale *= 0.5f;
            p *= 2.0f;
        }

        return total;
    }

	public static float Smoothstep(float edge0, float edge1, float x)
	{
		// Scale, bias and saturate x to 0..1 range
		x = Mathf.Clamp01((x - edge0)/(edge1 - edge0)); 
		// Evaluate polynomial
		return x*x*(3 - 2*x);
	}

}
