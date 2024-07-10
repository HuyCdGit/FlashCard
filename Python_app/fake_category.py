from faker import Faker
from connect_db import connect_to_database

fake = Faker()

def generate_fake_categories():
    # category_id = fake.unique.random_number(digits=1)
    category_name = fake.word()
        # categories.append((category_id, category_name))
    return (category_name)

def insert_fake_category(conn):
    cursor = conn.cursor()
    fake_category = [generate_fake_categories() for _ in range(3)]

    for category in fake_category:
        cursor.execute('''
            INSERT INTO category (categoryName)
            VALUES (?)
        ''', category)

    conn.commit()
    print("Inserted fake records into the Category table.")

if __name__ == "__main__":
    connection = connect_to_database()
    insert_fake_category(connection)
    connection.close()


#     import random
# from faker import Faker
# from connect_db import connect_to_database

# fake = Faker()
# CATEGORY_TYPE= ["lịch sử", "khoa học", "địa lý"]
# def generate_fake_categories():
#     category_id = fake.unique.random_number(digits=1)
#     category_name = random.choice(CATEGORY_TYPE)
#         # categories.append((category_id, category_name))
#     return (category_id,category_name)

# def check_category_exists(conn, category_name):
#     cursor = conn.cursor()
#     cursor.execute('SELECT COUNT(*) FROM category WHERE categoryName = ?', (category_name,))
#     count = cursor.fetchone()[0]
#     return count > 0

# def insert_fake_category(conn):
#     cursor = conn.cursor()
#     fake_category = [generate_fake_categories() for _ in range(3)]

#     for category in fake_category:
#         category_id, category_name = category
#         if not check_category_exists(conn, category_name):
#             cursor.execute('''
#                 INSERT INTO category (id, categoryName)
#                 VALUES (?, ?)
#             ''', category)

#     conn.commit()
#     print("Inserted fake records into the Flashcards table.")

# # Gọi hàm insert_fake_category(connection) để thực hiện chèn danh mục


# if __name__ == "__main__":
#     connection = connect_to_database()
#     insert_fake_category(connection)
#     connection.close()