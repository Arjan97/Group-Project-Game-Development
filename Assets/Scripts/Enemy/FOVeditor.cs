using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFOV))]
public class FOVeditor : Editor
{

    private void OnSceneGUI()
    {
        EnemyFOV fov = (EnemyFOV)target;
        Handles.color = Color.white;
       
        if (fov.showRadius) Handles.DrawWireArc(fov.transform.position + new Vector3(0,1,0), Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngleLeft = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        if (fov.showAngle)
        {
            Handles.color = Color.magenta;
            Handles.DrawLine(fov.transform.position + new Vector3(0, 1, 0), fov.transform.position + new Vector3(0, 1, 0) + viewAngleLeft * fov.radius);
            Handles.DrawLine(fov.transform.position + new Vector3(0, 1, 0), fov.transform.position + new Vector3(0, 1, 0) + viewAngleRight * fov.radius);
        }

        if (fov.playerInView && fov.showPlayerInView)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
