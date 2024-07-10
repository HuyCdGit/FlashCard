import random
from faker import Faker
from connect_db import connect_to_database

fake = Faker()
category_ids = [i for i in range(1, 4)]
user_ids = [i for i in range(1, 11)]
def generate_fake_flashcards():
    user_id = random.choice(user_ids)
    category_id = random.choice(category_ids)
    return (user_id, category_id)

def insert_fake_userCategory(conn):
    cursor = conn.cursor()
    fake_userCategory = [generate_fake_flashcards() for _ in range(20)]

    for userCategory in fake_userCategory:
        cursor.execute('''
            INSERT INTO user_category (user_id, category_id)
            VALUES (?, ?)
        ''', userCategory)

    conn.commit()
    print("Inserted fake records into the User_Category table.")

if __name__ == "__main__":
    connection = connect_to_database()
    insert_fake_userCategory(connection)
    connection.close()



    