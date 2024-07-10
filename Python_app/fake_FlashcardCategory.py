import random
from connect_db import connect_to_database

flashcard_ids = [i for i in range(1, 100)]
category_ids = [i for i in range(1, 4)]
def generate_fake_FlashcardCategory():
    flashcard_id = random.choice(flashcard_ids)
    category_id = random.choice(category_ids)
    return (flashcard_id, category_id)

def insert_fake_FlashcardCategory(conn):
    cursor = conn.cursor()
    fake_flashcardsCategory = [generate_fake_FlashcardCategory() for _ in range(100)]

    for flashcardsCategory in fake_flashcardsCategory:
        cursor.execute('''
            INSERT INTO flashcard_category (flashcard_id, category_id)
            VALUES (?, ?)
        ''', flashcardsCategory)

    conn.commit()
    print("Inserted fake records into the Flashcard_Category table.")

if __name__ == "__main__":
    connection = connect_to_database()
    insert_fake_FlashcardCategory(connection)
    connection.close()