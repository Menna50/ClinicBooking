### Role Design Decision

In this system, each user has exactly one role, stored as a simple `enum` (e.g., Admin, Doctor, Patient) within the `User` entity.

This approach is intentionally used to:

- Keep the architecture simple and maintainable
- Align with the current project scope (single-role per user)
- Avoid unnecessary complexity for a system that does not yet require dynamic role/permission management

### Why Not a Role Table or Many-to-Many Relationship?

While using a separate `Roles` table and supporting many-to-many relationships between users and roles is a more flexible and scalable design — it introduces extra complexity (e.g., user-role linking, role claims, dynamic permission resolution).

Since this system currently supports **one role per user**, the enum-based approach is cleaner, faster to implement, and fits the use case perfectly.

### Future Improvements

If the system grows to support features like:

- Assigning multiple roles to a user
- Managing roles dynamically from an admin interface
- Mapping fine-grained permissions per role

Then it can be extended to support a proper role-based access control (RBAC) model using:

- `Roles` table
- `UserRoles` many-to-many linking table
- Optional `Permissions` table (or Claims)

This would follow enterprise best practices for large-scale systems.
