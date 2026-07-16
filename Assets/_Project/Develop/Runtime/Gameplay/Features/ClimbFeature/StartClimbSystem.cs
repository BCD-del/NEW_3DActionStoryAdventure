using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ClimbFeature
{
    public class StartClimbSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startClimbingRequest;
        private ReactiveEvent _startClimbingEvent;
        private ReactiveVariable<bool> _inClimbingProcess;
        private Transform _transform;
        private ICompositeCondition _canStartClimbing;

        private IDisposable _climbingRequestDispose;

        //   private ReactiveVariable<Ray> _eyesRay;
        //   private ReactiveVariable<Vector3> _climbTargetPoint;
        //   private ReactiveVariable<Vector3> _climbTargetNormal;
        //   Позже переделай по нормальному (Рэй занеси туда куда надо)
        //   new Ray(new Vector3(0,0,0), Vector3.forward);


        public void OnInit(Entity entity)
        {
            _startClimbingRequest = entity.StartClimbingRequest;
            _startClimbingEvent = entity.StartClimbingEvent;
            _inClimbingProcess = entity.InClimbingProcess;
            _canStartClimbing = entity.CanStartClimbing;
            _transform = entity.Transform;
            _climbingRequestDispose = _startClimbingRequest.Subscribe(OnClimbingRequest);
        }

        private void OnClimbingRequest()
        {

            if (_canStartClimbing.Evaluate())
            {
                _inClimbingProcess.Value = true;
                _startClimbingEvent.Invoke();
                Debug.Log("Начало Вскарабкивания");
            }
            else
            {
                Debug.Log("Не Могу Начать Вскарабкиваться!");
            }
        }


        private void TryFindLedge(out Vector3 point, out Vector3 normal)
        {
            point = default;
            normal = default;

            Ray eyesRay = new Ray(_transform.position + Vector3.up * 0.5f, _transform.forward * 2);
            RaycastHit eyesHit;

            Ray upperRay = new Ray(_transform.position + Vector3.up * 1.8f, _transform.forward * 4);
            RaycastHit upperhit;

            Vector3 aboveLedgePoint = upperRay.origin + upperRay.direction * 4; //4 это upperRay distance (временно, потом всё будет через конфиг героя)


            if (Physics.Raycast(eyesRay.origin, eyesRay.direction, out eyesHit, Mathf.Infinity, 8) == true)
            {
                Debug.DrawRay(_transform.position, _transform.TransformDirection(Vector3.forward) * eyesHit.distance, Color.red);
            }

            bool upperRayBlocked = Physics.Raycast(upperRay.origin, upperRay.direction, out upperhit, Mathf.Infinity, 8);

            if (upperRayBlocked == false)
            {
                Debug.DrawRay(_transform.position, _transform.TransformDirection(Vector3.forward) * upperhit.distance, Color.yellow);
            }
        }

        public void OnDispose()
        {
            _climbingRequestDispose.Dispose();
        }

    }
}
