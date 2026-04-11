# System Design — Visão Geral

O processo inclui **duas** etapas de System Design distintas.

---

## Etapa 1: Work Simulation (~15 minutos)

Uma simulação de situações reais de trabalho como SDE II. Geralmente apresenta cenários onde você toma decisões técnicas, como:

- Escolher entre abordagens de design de sistemas
- Priorizar requisitos técnicos
- Identificar trade-offs entre soluções

**O que preparar:** entender conceitos fundamentais de design de sistemas (`concepts.md`) e como comunicar trade-offs.

---

## Etapa 2: Work Style (~5 minutos)

Perguntas sobre seu estilo de trabalho técnico:

- Como você lida com requisitos técnicos ambíguos?
- Como colabora com outros engenheiros?
- Como toma decisões de arquitetura?

**O que preparar:** reflita sobre suas experiências passadas com o método STAR. Veja também `03-behavioral/`.

---

## Princípios Fundamentais de System Design

Ao responder qualquer questão de design, demonstre que você pensa em:

1. **Escala** — o sistema funciona para 1 usuário? Para 1 bilhão?
2. **Disponibilidade** — o que acontece quando um componente falha?
3. **Consistência** — todos os usuários veem os mesmos dados?
4. **Latência** — quanto tempo cada operação leva?
5. **Custo** — qual é o custo operacional da solução?

---

## Framework para Responder Questões de Design

```
1. CLARIFICAÇÃO (2 min)
   - Quantos usuários? Qual throughput esperado?
   - Requisitos funcionais (o que o sistema faz)
   - Requisitos não-funcionais (latência, disponibilidade, consistência)

2. ESTIMATIVAS (1-2 min)
   - Requests per second (RPS)
   - Storage necessário
   - Bandwidth

3. API DESIGN (1-2 min)
   - Principais endpoints/operações

4. HIGH-LEVEL DESIGN (3-5 min)
   - Diagrama com componentes principais
   - Fluxo de dados

5. DEEP DIVE em componentes críticos (5 min)
   - Banco de dados? SQL ou NoSQL? Por quê?
   - Cache? Onde? O quê cachear?
   - Mensageria? Síncrono ou assíncrono?

6. TRADE-OFFS (1-2 min)
   - O que você sacrificou e por quê?
```

---

## Recursos para Aprofundar

- [System Design Primer (GitHub)](https://github.com/donnemartin/system-design-primer)
- [Grokking the System Design Interview](https://www.educative.io/courses/grokking-the-modern-system-design-interview)
- `02-system-design/concepts.md` — conceitos detalhados
- `02-system-design/work-simulation.md` — preparação específica para a simulação
