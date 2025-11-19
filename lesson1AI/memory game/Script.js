const grid = document.getElementById('game-grid');
const movesDisplay = document.getElementById('moves');
const restartBtn = document.getElementById('restart-btn');



// Icons/Emojis to match
const items = ['🚀', '🌟', '🎨', '⚓', '🌵', '💎', '🧩', '🔥'];
let cards = [];
let flippedCards = [];
let matchedPairs = 0;
let moves = 0;
let lockBoard = false;

// Initialize Game
function initGame() {
    // Duplicate items to create pairs and shuffle them
    const deck = [...items, ...items];
    shuffle(deck);
    
    // Reset State
    grid.innerHTML = '';
    cards = [];
    flippedCards = [];
    matchedPairs = 0;
    moves = 0;
    movesDisplay.innerText = moves;
    lockBoard = false;

    // Create HTML for cards
    deck.forEach(item => {
        const card = document.createElement('div');
        card.classList.add('card');
        
        // Create the inner structure for 3D flip
        card.innerHTML = `
            <div class="card-inner">
                <div class="card-front"></div>
                <div class="card-back">${item}</div>
            </div>
        `;

        // Add click event
        card.addEventListener('click', () => flipCard(card, item));
        
        grid.appendChild(card);
        cards.push(card);
    });
}

// Fisher-Yates Shuffle Algorithm
function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
}

// Flip Card Logic
function flipCard(card, value) {
    // Prevent action if board is locked or card is already flipped/matched
    if (lockBoard || card.classList.contains('flipped') || card.classList.contains('matched')) {
        return;
    }

    card.classList.add('flipped');
    flippedCards.push({ element: card, value: value });

    if (flippedCards.length === 2) {
        checkForMatch();
    }
}

// Check if the two flipped cards match
function checkForMatch() {
    lockBoard = true; // Lock board to prevent clicking a 3rd card
    moves++;
    movesDisplay.innerText = moves;

    const [card1, card2] = flippedCards;

    if (card1.value === card2.value) {
        // It's a match
        disableCards(card1.element, card2.element);
    } else {
        // Not a match
        unflipCards(card1.element, card2.element);
    }
}

function disableCards(card1, card2) {
    card1.classList.add('matched');
    card2.classList.add('matched');
    
    flippedCards = [];
    lockBoard = false;
    matchedPairs++;

    // Check for Win
    if (matchedPairs === items.length) {
        setTimeout(() => alert(`Victory! You won in ${moves} moves.`), 500);
    }
}

function unflipCards(card1, card2) {
    setTimeout(() => {
        card1.classList.remove('flipped');
        card2.classList.remove('flipped');
        flippedCards = [];
        lockBoard = false;
    }, 1000); // 1 second delay before flipping back
}

restartBtn.addEventListener('click', initGame);

// Start the game on load
initGame();