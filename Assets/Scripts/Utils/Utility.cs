using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void SwapTransform(Transform _t1, Transform _t2)
    {
        Vector3     tmp_pos = new Vector3(_t1.position.x, _t1.position.y, _t1.position.z);
        Quaternion  tmp_rot = new Quaternion(_t1.rotation.x, _t1.rotation.y, _t1.rotation.z, _t1.rotation.w);
        Vector3     tmp_sca = new Vector3(_t1.localScale.x, _t1.localScale.y, _t1.localScale.z);

        _t1.position = _t2.position;
        _t1.rotation = _t2.rotation;
        _t1.localScale = _t2.localScale;

        _t2.position = tmp_pos;
        _t2.rotation = tmp_rot;
        _t2.localScale = tmp_sca;
    }

    public static void SwapTransformLocal(Transform _t1, Transform _t2)
    {
        Vector3     tmp_pos = new Vector3(_t1.localPosition.x, _t1.localPosition.y, _t1.localPosition.z);
        Quaternion  tmp_rot = new Quaternion(_t1.localRotation.x, _t1.localRotation.y, _t1.localRotation.z, _t1.localRotation.w);
        Vector3     tmp_sca = new Vector3(_t1.localScale.x, _t1.localScale.y, _t1.localScale.z);

        _t1.localPosition = _t2.localPosition;
        _t1.localRotation = _t2.localRotation;
        _t1.localScale = _t2.localScale;

        _t2.localPosition = tmp_pos;
        _t2.localRotation = tmp_rot;
        _t2.localScale = tmp_sca;
    }

    public static void SwapParent(Transform _c1, Transform _c2, bool worldPositionStays)
    {
        Transform firstParent = _c1.parent;

        _c1.SetParent(_c2.parent, worldPositionStays);
        _c2.SetParent(firstParent, worldPositionStays);
    }
}
