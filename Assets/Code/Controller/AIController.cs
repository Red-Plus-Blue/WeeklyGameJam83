using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AIController : Controller
{
    public GameObject[] Path;

    protected List<Vector3> _path = new List<Vector3>();

    public Animator Animator;

    public void Run()
    {
        _path = Path.ToList().Select(point => point.transform.position).ToList();
        StartCoroutine(RunAI());
    }

    protected IEnumerator RunAI()
    {
        var remaining = new List<Vector3>();
        _path.ForEach(point => remaining.Add(point));

        Animator.SetBool("Walking", true);

        while (remaining.Count > 0)
        {
            var target = remaining[0];
            var distance = Vector3.Distance(target, gameObject.transform.position);

            while (distance > .1f)
            {
                var direction = (target - gameObject.transform.position).normalized;
                InputHorizontal = direction.x;
                InputVertical = direction.z;

                yield return null;
                distance = Vector3.Distance(target, gameObject.transform.position);
            }

            remaining.RemoveAt(0);
        }

        InputHorizontal = 0.0f;
        InputVertical = 0.0f;

        Animator.SetBool("Walking", false);
        Animator.SetTrigger("Attack");

        while(true)
        {
            Reactor.Instance.TakeDamage(Unit.Damage);
            yield return new WaitForSeconds(1.0f);
        }
    }

}