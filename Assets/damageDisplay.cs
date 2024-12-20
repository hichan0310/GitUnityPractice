﻿using System.Collections;
using System.Collections.Generic;
using GameBackend;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    private TextMeshPro tmp;

    // 데미지 텍스트 생성 함수
    public void ShowDamage(DmgEvent dmgEvent)
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.color = Color.black;
        tmp.text = dmgEvent.damage.ToString();
        if (dmgEvent.damageTags.Contains(Tags.atkTags.fireAttack))
        {
            tmp.color = Color.red;
            tmp.text = $"(bul){tmp.text}";
        }
        if (dmgEvent.damageTags.Contains(Tags.atkTags.physicalAttack))
        {
            tmp.color = Color.black;
            tmp.text = $"(muly){tmp.text}";
        }
        if (dmgEvent.damageTags.Contains(Tags.atkTags.normalAttack))
        {
            tmp.text = $"(normal){tmp.text}";
        }
        
        // 타겟의 위치에 약간 랜덤하게 배치
        tmp.transform.position = dmgEvent.target.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);

        // 애니메이션 코루틴 시작
        StartCoroutine(AnimateDamageText());
    }

    private IEnumerator AnimateDamageText()
    {
        float duration = 0.5f; // 애니메이션 시간
        float elapsed = 0f;

        Color initialColor = tmp.color;
        Vector3 initialScale = Vector3.one * 0.3f; // 초기 크기
        Vector3 targetScale = Vector3.one * 0.9f;  // 최종 크기

        tmp.transform.localScale = initialScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            // 크기를 점점 키움
            tmp.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            
            // 텍스트 투명도 점점 감소
            Color color = initialColor;
            color.a = Mathf.Lerp(1f, 0f, progress);
            tmp.color = color;

            yield return null;
        }

        // 애니메이션 종료 후 오브젝트 삭제
        Destroy(gameObject);
    }
}