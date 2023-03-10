/******************************************************************************
 * Copyright (C) Ultraleap, Inc. 2011-2020.                                   *
 *                                                                            *
 * Use subject to the terms of the Apache License 2.0 available at            *
 * http://www.apache.org/licenses/LICENSE-2.0, or another agreement           *
 * between Ultraleap and you, your company or other organization.             *
 ******************************************************************************/

using Leap.Unity.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Leap.Unity.Attachments {

  /// <summary>
  /// Simple container class for storing a reference to the attachment point this
  /// transform corresponds to within an AttachmentHand. Also contains mappings from
  /// a single AttachmentPointFlags flag constant to the relevant bone on a Leap.Hand;
  /// these mappings can be accessed statically via GetLeapHandPointData().
  /// 
  /// Can also be used to refer to a single AttachmentPointFlags flag constant (implicit conversion).
  /// </summary>
  [AddComponentMenu("")]
  [ExecuteInEditMode]
  public class AttachmentPointBehaviour : MonoBehaviour {

    [Tooltip("The AttachmentHand associated with this AttachmentPointBehaviour. AttachmentPointBehaviours "
           + "should be beneath their AttachmentHand object in the hierarchy.")]
    [Disable]
    public AttachmentHand attachmentHand;

    [Tooltip("To change which attachment points are available on an AttachmentHand, refer to the "
           + "inspector for the parent AttachmentHands object.")]
    [Disable]
    public AttachmentPointFlags attachmentPoint;

    void OnValidate() {
      if (!attachmentPoint.IsSinglePoint() && attachmentPoint != AttachmentPointFlags.None) {
        Debug.LogError("AttachmentPointBehaviours should refer to a single attachmentPoint flag.", this.gameObject);
        attachmentPoint = AttachmentPointFlags.None;
      }
    }

    void OnDestroy() {
      if (attachmentHand != null) {
        attachmentHand.notifyPointBehaviourDeleted(this);
      }
    }

    public static implicit operator AttachmentPointFlags(AttachmentPointBehaviour p) {
      if (p == null) return AttachmentPointFlags.None;
      return p.attachmentPoint;
    }

    public void SetTransformUsingHand(Leap.Hand hand) {
      if (hand == null) {
        //Debug.LogError("Unable to set transform with a null hand.", this.gameObject);
        return;
      }

            Vector3 position = Vector3.zero;
           // Vector3[] position = new Vector3[21];
            Quaternion rotation = Quaternion.identity;

      GetLeapHandPointData(hand, this.attachmentPoint, out position, out rotation);

      this.transform.position = position;
      this.transform.rotation = rotation;
    }

    public static void GetLeapHandPointData(Leap.Hand hand, AttachmentPointFlags singlePoint, out Vector3 position, out Quaternion rotation) {
      position = Vector3.zero;
      rotation = Quaternion.identity;

      if (singlePoint != AttachmentPointFlags.None && !singlePoint.IsSinglePoint()) {
        Debug.LogError("Cannot get attachment point data for an AttachmentPointFlags argument consisting of more than one set flag.");
        return;
      }

      switch (singlePoint) {
        case AttachmentPointFlags.None:
          return;

        case AttachmentPointFlags.Wrist:
          position = hand.WristPosition.ToVector3();
          rotation = hand.Arm.Basis.rotation.ToQuaternion();
                    //GameManager.instance.DetectGhost(0, position, rotation);
                    break;
        case AttachmentPointFlags.Palm:
          position = hand.PalmPosition.ToVector3();
          rotation = hand.Basis.rotation.ToQuaternion();
                    //GameManager.instance.DetectGhost(1, position, rotation);
                    break;

        case AttachmentPointFlags.ThumbProximalJoint:
          position = hand.Fingers[0].bones[1].NextJoint.ToVector3();
          rotation = hand.Fingers[0].bones[2].Rotation.ToQuaternion();
                    //GameManager.instance.DetectGhostFinger(0, 1, position, rotation);
                    break;

        case AttachmentPointFlags.ThumbDistalJoint:
          position = hand.Fingers[0].bones[2].NextJoint.ToVector3();
          rotation = hand.Fingers[0].bones[3].Rotation.ToQuaternion();
                    //GameManager.instance.DetectGhostFinger(0, 2, position, rotation);
                    break;

        case AttachmentPointFlags.ThumbTip:
          position = hand.Fingers[0].bones[3].NextJoint.ToVector3();
          rotation = hand.Fingers[0].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(0, 3, position, rotation);
                    break;
        case AttachmentPointFlags.IndexKnuckle:
          position = hand.Fingers[1].bones[0].NextJoint.ToVector3();
          rotation = hand.Fingers[1].bones[1].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(1, 0, position, rotation);
                    break;
        case AttachmentPointFlags.IndexMiddleJoint:
          position = hand.Fingers[1].bones[1].NextJoint.ToVector3();
          rotation = hand.Fingers[1].bones[2].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(1, 1, position, rotation);
                    break;
        case AttachmentPointFlags.IndexDistalJoint:
          position = hand.Fingers[1].bones[2].NextJoint.ToVector3();
          rotation = hand.Fingers[1].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(1, 2, position, rotation);
                    break;
        case AttachmentPointFlags.IndexTip:
          position = hand.Fingers[1].bones[3].NextJoint.ToVector3();
          rotation = hand.Fingers[1].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(1, 3, position, rotation);
                    break;

        case AttachmentPointFlags.MiddleKnuckle:
          position = hand.Fingers[2].bones[0].NextJoint.ToVector3();
          rotation = hand.Fingers[2].bones[1].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(2, 0, position, rotation);
                    break;
        case AttachmentPointFlags.MiddleMiddleJoint:
          position = hand.Fingers[2].bones[1].NextJoint.ToVector3();
          rotation = hand.Fingers[2].bones[2].Rotation.ToQuaternion();
                    //GameManager.instance.DetectGhostFinger(2, 1, position, rotation);
                    break;
        case AttachmentPointFlags.MiddleDistalJoint:
          position = hand.Fingers[2].bones[2].NextJoint.ToVector3();
          rotation = hand.Fingers[2].bones[3].Rotation.ToQuaternion();
                    //GameManager.instance.DetectGhostFinger(2, 2, position, rotation);
                    break;
        case AttachmentPointFlags.MiddleTip:
          position = hand.Fingers[2].bones[3].NextJoint.ToVector3();
          rotation = hand.Fingers[2].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(2, 3, position, rotation);
                    break;

        case AttachmentPointFlags.RingKnuckle:
          position = hand.Fingers[3].bones[0].NextJoint.ToVector3();
          rotation = hand.Fingers[3].bones[1].Rotation.ToQuaternion();
//GameManager.instance.DetectGhostFinger(3, 0, position, rotation);
                    break;
        case AttachmentPointFlags.RingMiddleJoint:
          position = hand.Fingers[3].bones[1].NextJoint.ToVector3();
          rotation = hand.Fingers[3].bones[2].Rotation.ToQuaternion();
                    //GameManager.instance.DetectGhostFinger(3, 1, position, rotation);
                    break;
        case AttachmentPointFlags.RingDistalJoint:
          position = hand.Fingers[3].bones[2].NextJoint.ToVector3();
          rotation = hand.Fingers[3].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(3, 2, position, rotation);

                    break;
        case AttachmentPointFlags.RingTip:
          position = hand.Fingers[3].bones[3].NextJoint.ToVector3();
          rotation = hand.Fingers[3].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(3, 3, position, rotation);

                    break;

        case AttachmentPointFlags.PinkyKnuckle:
          position = hand.Fingers[4].bones[0].NextJoint.ToVector3();
          rotation = hand.Fingers[4].bones[1].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(4, 0, position, rotation);

                    break;
        case AttachmentPointFlags.PinkyMiddleJoint:
          position = hand.Fingers[4].bones[1].NextJoint.ToVector3();
          rotation = hand.Fingers[4].bones[2].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(4, 1, position, rotation);

                    break;
        case AttachmentPointFlags.PinkyDistalJoint:
          position = hand.Fingers[4].bones[2].NextJoint.ToVector3();
          rotation = hand.Fingers[4].bones[3].Rotation.ToQuaternion();
                   // GameManager.instance.DetectGhostFinger(4, 2, position, rotation);

                    break;
        case AttachmentPointFlags.PinkyTip:
          position = hand.Fingers[4].bones[3].NextJoint.ToVector3();
          rotation = hand.Fingers[4].bones[3].Rotation.ToQuaternion();
                    //GameManager.instance.DetectGhostFinger(4, 3, position, rotation);

                    break;
      }
    }

  }

}
