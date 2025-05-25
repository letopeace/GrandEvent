using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Boostrap boostrap;
    public Animator animator;
    public CameraManager cameraM;
    public bool isPlayer;

    public Transform revolver, hand;
    public RevolverAnim revolverAnim;
    public Animation damageAnim;

    public Chip handChip, eyeChip, legChip, stomachChip, lungsChip, kidneyChip, liverChip, headChip;

    public int handCount = 2, eyeCount = 2, legCount = 4;
    public int stomach = 1, lungs = 1, kidney = 1, liver = 1, head = 1;

    public bool isHoldingRevolver = false;

    public List<ChipType> available_chips = new() {
        ChipType.hand, ChipType.eye, ChipType.leg, ChipType.stomach, ChipType.lungs,
        ChipType.kidney, ChipType.liver, ChipType.head
    };
    public List<ChipType> betted_chips = new();

    private bool temperaryTurn = true;
    public System.Random random = new System.Random();

    private void Update() { }

    public void Turn() { }

    public void Take()
    {
        animator.SetTrigger("Take");
        if (boostrap.miniRound == 1)
            animator.SetTrigger("Open");
    }

    public void Bid() { }
    public void Fraud() { }

    public void Shoot() { }

    public void RevolverHasTaken()
    {
        isHoldingRevolver = true;
        if (!isPlayer && boostrap.miniRound != 1)
        {
            if (random.Next(10) < 4) ShootYourSelf();
            else ShootOpponent();
        }
    }

    public void SpinAnimate(bool turn)
    {
        animator.SetTrigger("Spin");
        temperaryTurn = turn;
    }

    public void SpinAnimate2() => StartCoroutine(SpinRevolver(temperaryTurn));

    public void CameraFreeze() => cameraM.freeze = isPlayer;
    public void CameraUnfreeze() => cameraM.freeze = false;

    public void ShootOpponentCheck()
    {
        bool isShoot = revolverAnim.revolver.Shoot();
        Debug.Log(isShoot);

        if (isShoot)
        {
            revolverAnim.Shoot();
            (isPlayer ? boostrap.opponent : boostrap.player).DestroyRandomChip();
            if (!isPlayer) damageAnim.Play();

            if (boostrap.remainBullet == 0) boostrap.NextRound();
        }

        if (boostrap.remainBullet != 0) boostrap.NextTurn();
    }

    public void ShootYourSelfCheck()
    {
        bool isShoot = revolverAnim.revolver.Shoot();
        Debug.Log(isShoot);

        if (isShoot)
        {
            revolverAnim.Shoot();
            if (boostrap.remainBullet != 0) boostrap.NextTurn();
            (isPlayer ? boostrap.player : boostrap.opponent).DestroyRandomChip();
            if (isPlayer) damageAnim.Play();

            if (boostrap.remainBullet == 0) boostrap.NextRound();
        }
        else if (!boostrap.turn)
        {
            if (random.Next(10) < 4) ShootYourSelf();
            else ShootOpponent();
        }
    }

    public void ActivateShootingBottom()
    {
        if (boostrap.shoot && isPlayer)
            boostrap.ActivateShootingBottom();
    }

    public void ShootOpponent() => animator.SetTrigger("ShootOpponet");
    public void ShootYourSelf() => animator.SetTrigger("ShootYourSelf");

    public void TakeDamage(string chip)
    {
        if (Enum.TryParse(chip, out ChipType type))
        {
            ChipDestroy(type);
        }
    }

    public List<ChipType> BetChips(int count = 1)
    {
        List<ChipType> result = new();
        List<ChipType> tempChips = new(available_chips);

        if (count < tempChips.Count)
            tempChips.Remove(ChipType.head);

        for (int i = 0; i < count && tempChips.Count > 0; i++)
        {
            int ind = random.Next(tempChips.Count);
            ChipType chip = tempChips[ind];
            tempChips.RemoveAt(ind);
            result.Add(chip);
        }

        betted_chips = new(result);
        return result;
    }

    public void Betting(List<ChipType> chips)
    {
        animator.SetTrigger("Bet");
        StartCoroutine(ChipsBetAwait(0.6f, chips));
    }

    public void OpenDrums()
    {
        revolverAnim.OpenDrum();
        if (!isPlayer)
        {
            animator.SetTrigger("Close");
            boostrap.Spinner();
        }
    }

    public void CloseDrums() => revolverAnim.CloseDrum();
    public void SpinDrums() => revolverAnim.Spin();

    public void Reload()
    {
        revolverAnim.Initialize();
        revolverAnim.revolver.Randomize();
        revolverAnim.Clear();

        if (isPlayer)
            animator.SetTrigger("Close");
        else if (random.Next(10) < 4) ShootYourSelf();
        else ShootOpponent();
    }

    public void SetRandomBulletOrder()
    {
        Revolver newRevolver = new(boostrap.round);
        bool[] bullets = newRevolver.Show();

        revolverAnim.bullet_1.SetActive(bullets[0]);
        revolverAnim.bullet_2.SetActive(bullets[1]);
        revolverAnim.bullet_3.SetActive(bullets[2]);
        revolverAnim.bullet_4.SetActive(bullets[3]);
        revolverAnim.bullet_5.SetActive(bullets[4]);
        revolverAnim.bullet_6.SetActive(bullets[5]);
    }

    private IEnumerator ChipsBetAwait(float duration, List<ChipType> chips)
    {
        yield return new WaitForSeconds(duration);

        foreach (ChipType chip in chips)
        {
            GetChipObject(chip)?.Betting();
        }
    }

    public void BetChip(ChipType chip) => betted_chips.Add(chip);
    public void BetChipCancel(ChipType chip) => betted_chips.Remove(chip);

    public void DestroyRandomChip()
    {
        if (betted_chips.Count == 0) return;

        List<ChipType> chips = new(betted_chips);
        if (chips.Contains(ChipType.head) && chips.Count > 1)
            chips.Remove(ChipType.head);

        ChipDestroy(chips[random.Next(chips.Count)]);
    }

    private void ChipDestroy(ChipType type)
    {
        animator.SetTrigger("Damaged");

        switch (type)
        {
            case ChipType.hand: handCount = 0; break;
            case ChipType.eye: eyeCount = 0; break;
            case ChipType.leg: legCount = 0; break;
            case ChipType.stomach: stomach = 0; break;
            case ChipType.lungs: lungs = 0; break;
            case ChipType.kidney: kidney = 0; break;
            case ChipType.liver: liver = 0; break;
            case ChipType.head: head = 0; break;
        }

        GetChipObject(type)?.ChipDestroy();
    }

    private Chip GetChipObject(ChipType type) => type switch
    {
        ChipType.hand => handChip,
        ChipType.eye => eyeChip,
        ChipType.leg => legChip,
        ChipType.stomach => stomachChip,
        ChipType.lungs => lungsChip,
        ChipType.kidney => kidneyChip,
        ChipType.liver => liverChip,
        ChipType.head => headChip,
        _ => null
    };

    private IEnumerator SpinRevolver(bool turn)
    {
        Transform _revolver = revolver.parent;
        float duration = 2.5f;
        float startAngle = _revolver.eulerAngles.y;
        float targetOffset = turn ? -1350 : -1170f;
        float endAngle = startAngle + targetOffset;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float smoothT = 1 - Mathf.Pow(1 - t, 3);
            float currentAngle = Mathf.Lerp(startAngle, endAngle, smoothT);
            _revolver.eulerAngles = new Vector3(0, currentAngle, 0);
            yield return null;
        }

        _revolver.eulerAngles = new Vector3(0, endAngle, 0);
        yield return new WaitForSeconds(0.5f);
        boostrap.TakeRevolver();
    }

    private IEnumerator ConstrainRevolver()
    {
        isHoldingRevolver = true;
        while (isHoldingRevolver)
        {
            revolver.position = hand.position;
            revolver.rotation = hand.rotation;
            yield return null;
        }
        revolver.eulerAngles = Vector3.zero;
        revolver.position = new Vector3(-2.823f, 1.599f, -0.082f);
    }

    public void HoldRevolver() => StartCoroutine(ConstrainRevolver());
    public void DestroyConstrain() => isHoldingRevolver = false;
}

public enum ChipType
{
    hand, eye, leg, stomach, lungs, kidney, liver, head
}