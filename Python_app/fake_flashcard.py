import random
from faker import Faker
from connect_db import connect_to_database

fake = Faker()
def generate_fake_flashcards():
    # flashcards = []
    # flashcard_id = fake.unique.random_number(digits=3)
    term = fake.word()
    definition = fake.sentence()
    image = fake.url() if random.random() < 0.5 else None
    created_date = fake.date_this_decade()
    # flashcards.append((flashcard_id, term, user_id, definition, image, created_date))
    return (term, definition, image, created_date)

def insert_fake_flashcards(conn):
    cursor = conn.cursor()
    fake_flashcards = [generate_fake_flashcards() for _ in range(100)]

    for flashcard in fake_flashcards:
        cursor.execute('''
            INSERT INTO flashcards (term, definition, image, created_date)
            VALUES (?, ?, ?, ?)
        ''', flashcard)

    conn.commit()
    print("Inserted fake records into the Flashcards table.")

if __name__ == "__main__":
    connection = connect_to_database()
    insert_fake_flashcards(connection)
    connection.close()



    