using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{
    public class MemberService_unitTest
    {
        private IMembersRepository membersRepository;
        private IEmpoloyeeRepositroy empoloyeeRepositroy;
        private ILibraryRepository libraryRepository;
        private MemberService memberService;
        private EmployeeService employeeService;

        public MemberService_unitTest()
        {
            membersRepository = A.Fake<IMembersRepository>();
            empoloyeeRepositroy = A.Fake<IEmpoloyeeRepositroy>();
            libraryRepository = A.Fake<ILibraryRepository>();
            employeeService = new EmployeeService(empoloyeeRepositroy, null, null);
            memberService = new MemberService(membersRepository, libraryRepository, employeeService, null, null);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_ValidCredentials_SetsLoggedUser()
        {
            // Arrange
            string username = "user1";
            string password = "password1";
            var returnedMembers = new List<Member>
        {
            new Member { username = username, password = password, Library_id = 1 }
        }.AsQueryable();

            A.CallTo(() => membersRepository.GetMemberLogin(username, password)).Returns(returnedMembers);

            // Act
            memberService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Member, LoggedUser.UserType);
            Assert.Equal(1, LoggedUser.LibraryId);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_InvalidCredentials_DoesNotSetLoggedUser()
        {
            // Arrange
            string username = "user1";
            string password = "password1";
            var returnedMembers = new List<Member>().AsQueryable();

            A.CallTo(() => membersRepository.GetMemberLogin(username, password)).Returns(returnedMembers);

            // Act
            memberService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckMembershipDateLogin_ValidMembership_ReturnsFalse()
        {
            // Arrange
            string username = "user1";
            string password = "password1";
            var member = new Member
            {
                username = username,
                password = password,
                Library_id = 1,
                membership_date = DateTime.Now.AddDays(-365)
            };
            A.CallTo(() => membersRepository.GetMemberLogin(username, password)).Returns(new List<Member> { member }.AsQueryable());

            // Act
            bool result = memberService.CheckMembershipDateLogin(username, password);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markoivinović
        [Fact]
        public void CheckMembershipDateLogin_ExpiredMembership_ReturnsTrue()
        {
            // Arrange
            string username = "user1";
            string password = "password1";
            var member = new Member
            {
                username = username,
                password = password,
                Library_id = 1,
                membership_date = DateTime.Now.AddDays(-400) // Membership expired 400 days ago (approximately 13 months)
            };
            A.CallTo(() => membersRepository.GetMemberLogin(username, password)).Returns(new List<Member> { member }.AsQueryable());

            // Act
            bool result = memberService.CheckMembershipDateLogin(username, password);

            // Assert
            Assert.True(result); // Membership has expired
        }
        [Fact]
        public void CheckMembershipDateLogin_MemberDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string username = "nonexistent_user";
            string password = "nonexistent_password";
            A.CallTo(() => membersRepository.GetMemberLogin(username, password)).Returns(new List<Member>().AsQueryable());

            // Act
            bool result = memberService.CheckMembershipDateLogin(username, password);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckExistingUsername_UsernameExists_ReturnsTrue()
        {
            // Arrange
            string existingUsername = "existing_user";
            var existingMember = new Member { username = existingUsername };
            var existingMembers = new List<Member> { existingMember };
            A.CallTo(() => membersRepository.GetAll()).Returns(existingMembers.AsQueryable());

            // Act
            bool result = memberService.CheckExistingUsername(existingMember);

            // Assert
            Assert.True(result);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckExistingUsername_UsernameDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string nonExistingUsername = "non_existing_user";
            var nonExistingMember = new Member { username = nonExistingUsername };
            var existingMembers = new List<Member>();
            A.CallTo(() => membersRepository.GetAll()).Returns(existingMembers.AsQueryable());

            // Act
            bool result = memberService.CheckExistingUsername(nonExistingMember);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckExistingUsername_EmptyMemberList_ReturnsFalse()
        {
            // Arrange
            var member = new Member { username = "any_username" };
            var emptyMemberList = new List<Member>();
            A.CallTo(() => membersRepository.GetAll()).Returns(emptyMemberList.AsQueryable());

            // Act
            bool result = memberService.CheckExistingUsername(member);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markovinović
        [Fact]

        public void CheckBarcodeUnique_BarcodeExists_ReturnsTrue()
        {
            // Arrange
            string existingBarcodeId = "existing_barcode";
            var existingMember = new Member { barcode_id = existingBarcodeId };
            var existingMembers = new List<Member> { existingMember };
            A.CallTo(() => membersRepository.GetAll()).Returns(existingMembers.AsQueryable());

            // Act
            bool result = memberService.CheckBarcodeUnoque(existingMember);

            // Assert
            Assert.True(result);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckBarcodeUnique_BarcodeDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string nonExistingBarcodeId = "non_existing_barcode";
            var nonExistingMember = new Member { barcode_id = nonExistingBarcodeId };
            var existingMembers = new List<Member>();
            A.CallTo(() => membersRepository.GetAll()).Returns(existingMembers.AsQueryable());

            // Act
            bool result = memberService.CheckBarcodeUnoque(nonExistingMember);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckBarcodeUnique_EmptyMemberList_ReturnsFalse()
        {
            // Arrange
            var member = new Member { barcode_id = "any_barcode" };
            var emptyMemberList = new List<Member>();
            A.CallTo(() => membersRepository.GetAll()).Returns(emptyMemberList.AsQueryable());

            // Act
            bool result = memberService.CheckBarcodeUnoque(member);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckOibUnique_OIBExists_ReturnsTrue()
        {
            // Arrange
            string existingOIB = "existing_OIB";
            var existingMember = new Member { OIB = existingOIB };
            var existingMembers = new List<Member> { existingMember };
            A.CallTo(() => membersRepository.GetAll()).Returns(existingMembers.AsQueryable());

            // Act
            bool result = memberService.CheckOibUnoque(existingMember);

            // Assert
            Assert.True(result);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckOibUnique_OIBDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string nonExistingOIB = "non_existing_OIB";
            var nonExistingMember = new Member { OIB = nonExistingOIB };
            var existingMembers = new List<Member>();
            A.CallTo(() => membersRepository.GetAll()).Returns(existingMembers.AsQueryable());

            // Act
            bool result = memberService.CheckOibUnoque(nonExistingMember);

            // Assert
            Assert.False(result);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckOibUnique_EmptyMemberList_ReturnsFalse()
        {
            // Arrange
            var member = new Member { OIB = "any_OIB" };
            var emptyMemberList = new List<Member>();
            A.CallTo(() => membersRepository.GetAll()).Returns(emptyMemberList.AsQueryable());

            // Act
            bool result = memberService.CheckOibUnoque(member);

            // Assert
            Assert.False(result);
        }
        //Magdalena Markovinović
        [Fact]
        public void AddNewMember_ValidMember_ReturnsTrue()
        {
            // Arrange
            var memberToAdd = new Member
            {
                id = 1,
                name = "test1",
                surname = "test1",
                OIB = "12345678901",
                membership_date = DateTime.Now,
                barcode_id = "123456",
                username = "test1",
                password = "password1",
                Library_id = 1
            };

            // Act
            bool result = memberService.AddNewMember(memberToAdd);

            // Assert
            A.CallTo(() => membersRepository.Add(memberToAdd)).MustHaveHappenedOnceExactly();
            Assert.True(result); 
        }
        //Magdalena Markovinović
        [Fact]
        public void UpdateMember_SuccessfulUpdate_ReturnsTrue()
        {
            // Arrange
            var memberToUpdate = new Member
            {
                id = 1,
                name = "test2",
                surname = "test2",
                OIB = "12345678901",
                membership_date = DateTime.Now,
                barcode_id = "123456",
                username = "test1",
                password = "password2",
                Library_id = 1
            };

            A.CallTo(() => membersRepository.Update(memberToUpdate, true)).Returns(1);

            // Act
            bool result = memberService.UpdateMember(memberToUpdate);

            // Assert
            Assert.True(result);
        }

        [Fact]
        //Magdalena Markovinović
        public void UpdateMember_UnsuccessfulUpdate_ReturnsFalse()
        {
            // Arrange
            var memberToUpdate = new Member
            {
                id = 1,
                name = "test2",
                surname = "test2",
                OIB = "12345678901",
                membership_date = DateTime.Now,
                barcode_id = "123456",
                username = "test2",
                password = "password2",
                Library_id = 1
            };

            A.CallTo(() => membersRepository.Update(memberToUpdate, false)).Returns(0);

            // Act
            bool result = memberService.UpdateMember(memberToUpdate);

            // Assert
            Assert.False(result);
        }

        //Magdalena Markovinović
        [Fact]
        public void DeleteMember_BorrowsAndReservationsClear_ReturnsTrue()
        {
            // TODO: Implement this test
            Assert.True(false);
        }

        //Magdalena Markovinović
        [Fact]
        public void GetAllMembersByFilter_WithValidNameAndSurname_ReturnsFilteredMembers()
        {
            // Arrange
            string name = "John";
            string surname = "Doe";
            var filteredMembers = new List<Member>
            {
                new Member { id = 1, name = "test1", surname = "test1" },
                new Member { id = 2, name = "tets2", surname = "tets2" }
            };
            A.CallTo(() => membersRepository.GetAllMembersByFilter(name, surname)).Returns(filteredMembers.AsQueryable());

            // Act
            var result = memberService.GetAllMembersByFilter(name, surname);

            // Assert
            Assert.Equal(filteredMembers.Count, result.Count);
            Assert.All(result, m => Assert.Contains(m, filteredMembers));
        }

        //Magdalena Markovinović
        [Fact]
        public void GetAllMembersByFilter_WithInvalidNameAndSurname_ReturnsEmptyList()
        {
            // Arrange
            string name = "NonExistent";
            string surname = "Person";
            A.CallTo(() => membersRepository.GetAllMembersByFilter(name, surname)).Returns(new List<Member>().AsQueryable());

            // Act
            var result = memberService.GetAllMembersByFilter(name, surname);

            // Assert
            Assert.Empty(result);
        }

        //Magdalena Markovinović
        [Fact]
        public void GetAllMembersByFilter_WithEmptyNameAndSurname_ReturnsAllMembers()
        {
            // Arrange
            string name = string.Empty;
            string surname = string.Empty;
            var allMembers = new List<Member>
            {
                new Member { id = 1, name = "test1", surname = "test1" },
                new Member { id = 2, name = "tets2", surname = "tets2" }
            };
            A.CallTo(() => membersRepository.GetAllMembersByFilter(name, surname)).Returns(allMembers.AsQueryable());

            // Act
            var result = memberService.GetAllMembersByFilter(name, surname);

            // Assert
            Assert.Equal(allMembers.Count, result.Count); 
            Assert.All(allMembers, m => Assert.Contains(m, result));
        }
        //Magdalena Markovinović
        [Fact]
        public void GetMemberId_ExistingUsername_ReturnsMemberId()
        {
            // Arrange
            string existingUsername = "existing_username";
            int expectedId = 1;
            A.CallTo(() => membersRepository.GetMemberId(existingUsername)).Returns(expectedId);

            // Act
            int result = memberService.GetMemberId(existingUsername);

            // Assert
            Assert.Equal(expectedId, result);
        }

        //Magdalena Markovinović
        [Fact]
        public void GetMemberId_NonExistingUsername_ReturnsZero()
        {
            // Arrange
            string nonExistingUsername = "non_existing_username";
            int expectedId = 0;
            A.CallTo(() => membersRepository.GetMemberId(nonExistingUsername)).Returns(expectedId);

            // Act
            int result = memberService.GetMemberId(nonExistingUsername);

            // Assert
            Assert.Equal(expectedId, result);
        }
        //Magdalena Markovinović
        [Fact]
        public void GetMemberNameSurname_ExistingMemberId_ReturnsMemberNameSurname()
        {
            // Arrange
            int existingMemberId = 1;
            var expectedNameSurname = new List<string> { "test1", "test1" }.AsQueryable();
            A.CallTo(() => membersRepository.GetMemberNameSurname(existingMemberId)).Returns(expectedNameSurname);

            // Act
            var result = memberService.GetMemberNameSurname(existingMemberId);

            // Assert
            Assert.Equal(expectedNameSurname, result);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberNameSurname_NonExistingMemberId_ReturnsEmptyQueryable()
        {
            // Arrange
            int nonExistingMemberId = 999;
            IQueryable<string> expected = Enumerable.Empty<string>().AsQueryable();
            A.CallTo(() => membersRepository.GetMemberNameSurname(nonExistingMemberId)).Returns(expected);

            // Act
            var result = memberService.GetMemberNameSurname(nonExistingMemberId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberLibraryId_ExistingUsername_ReturnsLibraryId()
        {
            // Arrange
            string existingUsername = "existing_username";
            int expectedLibraryId = 1;
            A.CallTo(() => membersRepository.GetMemberLibraryId(existingUsername)).Returns(expectedLibraryId);

            // Act
            int result = memberService.GetMemberLibraryId(existingUsername);

            // Assert
            Assert.Equal(expectedLibraryId, result);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberLibraryId_NonExistingUsername_ReturnsZero()
        {
            // Arrange
            string nonExistingUsername = "non_existing_username";
            int expectedLibraryId = 0;
            A.CallTo(() => membersRepository.GetMemberLibraryId(nonExistingUsername)).Returns(expectedLibraryId);

            // Act
            int result = memberService.GetMemberLibraryId(nonExistingUsername);

            // Assert
            Assert.Equal(expectedLibraryId, result);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberByUsername_ExistingUsername_ReturnsMember()
        {
            // Arrange
            string existingUsername = "existing_username";
            var expectedMember = new Member
            {
                id = 1,
                name = "test2",
                surname = "test2",
                OIB = "12345678901",
                membership_date = DateTime.Now,
                barcode_id = "123456",
                username = "test2",
                password = "password2",
                Library_id = 1
            };
            A.CallTo(() => membersRepository.GetMembersByUsername(existingUsername)).Returns(new List<Member> { expectedMember }.AsQueryable());

            // Act
            var result = memberService.GetMemberByUsername(existingUsername);

            // Assert
            Assert.Equal(expectedMember, result);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberByUsername_NonExistingUsername_ThrowsException()
        {
            // Arrange
            string nonExistingUsername = "non_existing_username";
            A.CallTo(() => membersRepository.GetMembersByUsername(nonExistingUsername)).Returns(Enumerable.Empty<Member>().AsQueryable());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => memberService.GetMemberByUsername(nonExistingUsername));
        }

        //Magdalena Markovinović
        [Fact]
        public void RandomCodeGenerator_ReturnsEightCharacterString()
        {
            // Arrange
            var memberService = new MemberService();

            // Act
            string result = memberService.RandomCodeGenerator();

            // Assert
            Assert.Equal(8, result.Length);
        }

        //Magdalena Markovinović
        [Fact]
        public void RandomCodeGenerator_ReturnsAlphanumericString()
        {
            // Arrange
            var memberService = new MemberService();

            // Act
            string result = memberService.RandomCodeGenerator();

            // Assert
            Assert.Matches("^[a-zA-Z0-9]*$", result);
        }
        //Magdalena Markovinović
        [Fact]
        public void GetAllMembersByLibrary_WithValidLibraryId_ReturnsMembers()
        {
            // Arrange
            int libraryId = 1;
            var expectedMembers = new List<Member>
            {
                new Member { id = 1, name = "John", surname = "Doe", Library_id = 1 },
                new Member { id = 2, name = "Jane", surname = "Smith", Library_id = 1 }
            };
            A.CallTo(() => employeeService.GetEmployeeLibraryId(A<string>._)).Returns(libraryId);
            A.CallTo(() => membersRepository.GetMembersByLibrary(libraryId)).Returns(expectedMembers.AsQueryable());

            // Act
            var result = memberService.GetAllMembersByLybrary();

            // Assert
            Assert.Equal(expectedMembers.Count, result.Count);
            Assert.All(result, m => Assert.Contains(m, expectedMembers));
        }

        [Fact]
        //Magdalena Markovinović
        public void GetAllMembersByLibrary_WithInvalidLibraryId_ReturnsEmptyList()
        {
            // Arrange
            int invalidLibraryId = -1;
            A.CallTo(() => employeeService.GetEmployeeLibraryId(A<string>._)).Returns(invalidLibraryId);

            // Act
            var result = memberService.GetAllMembersByLybrary();

            // Assert
            Assert.Empty(result);
        }

        // TODO testovi za GetMemberByBarcodeId(@dmatijani21)

        //Magdalena Markovinović
        [Fact]
        public void GetMemberBarcode_WithValidId_ReturnsBarcode()
        {
            // Arrange
            int memberId = 1; // Assuming a valid member ID
            string expectedBarcode = "ABCD1234";
            A.CallTo(() => membersRepository.GetMemberBarcode(memberId)).Returns(new List<string> { expectedBarcode }.AsQueryable());

            // Act
            string result = memberService.GetMemberBarcode(memberId);

            // Assert
            Assert.Equal(expectedBarcode, result);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberBarcode_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidId = -1;
            A.CallTo(() => membersRepository.GetMemberBarcode(invalidId)).Returns(new List<string>().AsQueryable()); 

            // Act
            string result = memberService.GetMemberBarcode(invalidId);

            // Assert
            Assert.Null(result);
        }

        //Magdalena Markovinović
        [Fact]
        public void GetMemberByBarcodeId_ValidLibraryIdAndBarcodeId_ReturnsMember()
        {
            // Arrange
            int libraryId = 1;
            string barcodeId = "ABCD1234";
            var expectedMember = new Member
            {
                id = 1,
                name = "test1",
                surname = "test1",
                barcode_id = barcodeId,
                Library_id = libraryId,
                Library = new Library { id = libraryId }
            };
            A.CallTo(() => membersRepository.GetMemberByBarcodeId(barcodeId)).Returns(new List<Member> { expectedMember }.AsQueryable());

            // Act
            Member result = memberService.GetMemberByBarcodeId(libraryId, barcodeId);

            // Assert
            Assert.Equal(expectedMember, result);
        }
        [Fact]
        //Magdalena Markovinović
        public void GetMemberByBarcodeId_NonExistentBarcodeId_ThrowsMemberNotFoundException()
        {
            // Arrange
            int libraryId = 1;
            string barcodeId = "NONEXISTENT";
            A.CallTo(() => membersRepository.GetMemberByBarcodeId(barcodeId)).Returns(new List<Member>().AsQueryable());

            // Act
            var exception = Assert.Throws<MemberNotFoundException>(() => memberService.GetMemberByBarcodeId(libraryId, barcodeId));

            // Assert
            Assert.Equal("Član knjižnice sa tim barkodom ne postoji!", exception.Message);
        }

        [Fact]
        //Magdalena Markovinović
        public void GetMemberByBarcodeId_WrongLibraryId_ThrowsWrongLibraryException()
        {
            // Arrange
            int libraryId = 1;
            string barcodeId = "ABCD1234";
            var memberFromAnotherLibrary = new Member
            {
                id = 1,
                name = "test1",
                surname = "test1",
                barcode_id = barcodeId,
                Library_id = 2,
                Library = new Library { id = 2 }
            };
            A.CallTo(() => membersRepository.GetMemberByBarcodeId(barcodeId)).Returns(new List<Member> { memberFromAnotherLibrary }.AsQueryable());

            // Act
            var exception = Assert.Throws<WrongLibraryException>(() => memberService.GetMemberByBarcodeId(libraryId, barcodeId));

            // Assert
            Assert.Equal("Član ove knjižnice s tim barkodom ne postoji!", exception.Message);
        }
    }
}
