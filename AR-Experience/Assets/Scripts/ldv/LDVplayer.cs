using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDVplayer : MonoBehaviour
{
  public float m_speed = 1.0f;
  public float m_inclinationSpeed = 0.125f;
  public float m_jumpPower = 1.0f;
  public float m_limitDown = 1.0f;
  public bool m_active = false;
  public bool m_grounded = true;
  public Vector3 m_startPosition;

  Vector3 m_currDirection;
  Vector3 m_right = Vector3.right;
  Vector3 m_inclinationUp = (Vector3.up + Vector3.right).normalized;
  Vector3 m_inclinationDown = (Vector3.down + Vector3.right).normalized;

  Rigidbody m_rb;

  public RaycastHit lr;
  public LayerMask layer;

  [Header("Animation settings")]
  public Animator m_animator;

  // Start is called before the first frame update
  public void onStart()
  {
    m_startPosition = transform.position;
    m_rb = GetComponent<Rigidbody>();
    m_currDirection = m_right;
  }

  // Update is called once per frame
  public void onUpdate()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      m_active = true;
    }

    if (m_active)
    {
      m_animator.SetBool("walk", true);
    }
    else
    {
      m_animator.SetBool("walk", false);
    }


    if (Physics.Raycast(transform.position + transform.forward * 0.02f, -transform.up, out lr,0.7f, layer))
    {
      m_grounded = true;
      Debug.DrawRay(lr.point, Vector3.up);
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, lr.normal), lr.normal), Time.deltaTime * 5);
      if (lr.transform.tag == "RampUp")
      {
        m_currDirection = m_inclinationUp;
      }
      else if (lr.transform.tag == "RampDown")
      {
        m_currDirection = m_inclinationDown;
      }
      else
      {
        m_currDirection = m_right;
      }
    }
    else
    {
      m_grounded = false;
    }

    if (transform.position.y < m_limitDown)
    {
      FindObjectOfType<LDVTempGameManager>().changePlayStop();
      reset();
    }
  }

  private void FixedUpdate()
  {
    if (m_active && m_grounded)
    {
      Vector3 currVel = m_rb.velocity;
      currVel.x = m_currDirection.x * m_speed;
      currVel.y = (m_currDirection.y * m_inclinationSpeed) + m_rb.velocity.y;
      m_rb.velocity = currVel;
    }
  }

  public void Jump()
  {
    Vector3 currVel = m_rb.velocity;
    currVel.y = 0.0f;
    m_rb.velocity = currVel;
    AudioManager.playSound(Sounds.jump, 1.0f);
    m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
    m_animator.SetTrigger("jump");
  }

  public void active()
  {
    m_active = true;
  }

  public void reset()
  {
    m_active = false;
    transform.position = m_startPosition;
    m_rb.velocity = Vector3.zero;
    transform.eulerAngles = new Vector3(0,90,0);
  }

}
