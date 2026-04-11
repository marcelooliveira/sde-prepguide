# Conceitos Fundamentais de System Design

Referência rápida dos tópicos mais cobrados em system design de SDE II.

---

## 1. Escalabilidade

### Vertical vs. Horizontal
| | Vertical (Scale Up) | Horizontal (Scale Out) |
|---|---|---|
| O que é | Hardware mais potente | Mais instâncias |
| Limite | Limite físico da máquina | Quase ilimitado |
| Custo | Caro em escala | Mais flexível |
| Complexidade | Baixa | Alta (precisa de distribuição) |

### Stateless vs. Stateful
- **Stateless:** servidor não guarda estado entre requests → fácil de escalar horizontalmente
- **Stateful:** servidor guarda sessão → difícil de escalar, requer sticky sessions ou armazenamento externo

---

## 2. Load Balancing

Distribui tráfego entre múltiplos servidores.

**Algoritmos comuns:**
- **Round Robin** — alterna entre servidores em sequência
- **Least Connections** — manda para o servidor com menos conexões ativas
- **IP Hash** — mesmo usuário sempre vai para o mesmo servidor (útil para sessões)
- **Weighted** — servidores mais potentes recebem mais tráfego

**Onde usar:** entre client e application servers; entre application servers e databases.

---

## 3. Caching

Armazena resultados de operações custosas para reutilização.

### Políticas de Invalidação
| Política | Descrição | Quando usar |
|---|---|---|
| **TTL (Time-To-Live)** | Cache expira após X segundos | Dados que mudam com frequência previsível |
| **LRU (Least Recently Used)** | Descarta os menos acessados recentemente | Cache de tamanho fixo em memória |
| **Write-Through** | Escreve no cache e no DB simultaneamente | Consistência forte |
| **Write-Back** | Escreve só no cache, sincroniza com DB depois | Performance, risco de perda |

### Onde Cachear
- **CDN** — conteúdo estático (imagens, CSS, JS) próximo ao usuário
- **Reverse Proxy** (Nginx, Varnish) — respostas HTTP completas
- **Application Cache** (Redis, Memcached) — objetos computados, sessões, contadores
- **Database Cache** — query plans, resultados de queries frequentes

---

## 4. Banco de Dados

### SQL vs. NoSQL

| | SQL | NoSQL |
|---|---|---|
| Estrutura | Esquema rígido | Flexível / schema-less |
| Consultas | Joins complexos, ACID | Simples, escalável horizontalmente |
| Exemplos | PostgreSQL, MySQL | MongoDB, Cassandra, DynamoDB |
| Quando usar | Dados relacionais, transações | Alto volume, estrutura variável, escala |

### Índices
- Aceleram leitura, aumentam custo de escrita
- Crie índices nas colunas usadas em `WHERE`, `JOIN`, `ORDER BY`
- Índices compostos: `(coluna_a, coluna_b)` — ordem importa

### Sharding (Particionamento Horizontal)
- Dividir dados em múltiplos bancos por chave (user_id, região)
- Reduz carga por banco, mas complica joins e transações entre shards

### Replicação
- **Master-Slave:** gravações no master, leituras nos slaves → maior throughput de leitura
- **Multi-Master:** múltiplos masters → mais complexo, risco de conflitos

---

## 5. Mensageria (Message Queues)

Desacopla produtores e consumidores. Exemplos: Kafka, RabbitMQ, SQS.

**Padrões:**
- **Pub/Sub:** produtor publica no tópico, múltiplos consumidores recebem a mensagem
- **Work Queue:** múltiplos workers competem por mensagens — cada mensagem processada por apenas um

**Benefícios:**
- Absorve picos de tráfego (buffer)
- Permite processamento assíncrono
- Desacopla serviços (produtor não precisa saber dos consumidores)

---

## 6. Microserviços vs. Monolito

| | Monolito | Microserviços |
|---|---|---|
| Deploy | Tudo junto | Independente por serviço |
| Desenvolvimento | Simples no início | Complexidade de rede e coordenação |
| Escala | Escala o todo | Escala serviços individualmente |
| Quando preferir | Equipe pequena, fase inicial | Equipes grandes, alta escala |

---

## 7. CAP Theorem

Em um sistema distribuído, só é possível garantir **2 dos 3**:

- **C** — Consistency: todos os nós veem os mesmos dados simultaneamente
- **A** — Availability: o sistema responde sempre, mesmo com falhas
- **P** — Partition Tolerance: o sistema continua funcionando mesmo com falhas de rede entre nós

> Na prática, P é obrigatório em sistemas distribuídos → você escolhe entre **CP** ou **AP**.

| Sistema | CP ou AP | Exemplo |
|---|---|---|
| PostgreSQL (único nó) | CA | Banco relacional local |
| MongoDB | CP | Consistência forte com réplicas |
| Cassandra | AP | Alta disponibilidade, eventual consistency |
| DynamoDB | AP (configurável) | Amazon, eventual consistency default |

---

## 8. APIs e Protocolos

### REST
- Stateless, usa HTTP verbs (GET, POST, PUT, DELETE)
- JSON como formato padrão
- Simples e amplamente adotado

### GraphQL
- Cliente especifica exatamente quais campos quer
- Evita over-fetching e under-fetching
- Mais complexo de implementar no servidor

### gRPC
- Protobuf binário — mais eficiente que JSON
- Chamadas tipadas e contratos claros
- Ideal para comunicação interna entre microserviços

---

## 9. Estimativas de Capacidade (Back-of-the-Envelope)

| Grandeza | Valor aproximado |
|---|---|
| 1 KB | 1.000 bytes |
| 1 MB | 10⁶ bytes |
| 1 GB | 10⁹ bytes |
| 1 TB | 10¹² bytes |
| 1 bilhão users, 1 req/dia | ~11.600 RPS |
| Latência SSD | ~0.1 ms |
| Latência Redis | ~0.5 ms |
| Latência DB (rede) | ~5-10 ms |
| Latência inter-datacenter | ~50-300 ms |

---

## 10. Disponibilidade (Nines)

| Disponibilidade | Downtime/ano |
|---|---|
| 99% (dois nines) | ~3,6 dias |
| 99,9% (três nines) | ~8,7 horas |
| 99,99% (quatro nines) | ~52 minutos |
| 99,999% (cinco nines) | ~5 minutos |
