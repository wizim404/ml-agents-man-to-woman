using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizimAcademy : Academy
{
	public static float scale = 0.5f;
    public override void AcademyReset()
    {
		scale = (float)resetParameters["scale"];
    }

    public override void AcademyStep()
    {
		
    }
}
