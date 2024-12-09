using System;

using Championship.Models;
using NUnit.Framework;

namespace Championship.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAddTeam_Success()
        {
            League league = new League();
            Team team = new Team("Team1");

            league.AddTeam(team);

            Assert.That(league.Teams.Count, Is.EqualTo(1));
            Assert.That(league.Teams[0].Name, Is.EqualTo("Team1"));
        }

        [Test]
        public void TestAddTeam_LeagueFull()
        {
            League league = new League();
            for (int i = 0; i < league.Capacity; i++)
            {
                league.AddTeam(new Team($"Team{i}"));
            }

            var ex = Assert.Throws<InvalidOperationException>(() => league.AddTeam(new Team("OverflowTeam")));
            Assert.That(ex.Message, Is.EqualTo("League is full."));
        }

        [Test]
        public void TestAddTeam_TeamAlreadyExists()
        {
            League league = new League();
            league.AddTeam(new Team("Team1"));

            var ex = Assert.Throws<InvalidOperationException>(() => league.AddTeam(new Team("Team1")));
            Assert.That(ex.Message, Is.EqualTo("Team already exists."));
        }

        [Test]
        public void TestRemoveTeam_Success()
        {
            League league = new League();
            league.AddTeam(new Team("Team1"));
            league.AddTeam(new Team("Team2"));

            var result = league.RemoveTeam("Team1");

            Assert.That(result, Is.True);
            Assert.That(league.Teams.Count, Is.EqualTo(1));
            Assert.That(league.Teams[0].Name, Is.EqualTo("Team2"));
        }

        [Test]
        public void TestRemoveTeam_TeamDoesNotExist()
        {
            League league = new League();
            league.AddTeam(new Team("Team1"));

            var result = league.RemoveTeam("Team2");
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);
                Assert.That(league.Teams.Count, Is.EqualTo(1));
            });
            Assert.That(league.Teams[0].Name, Is.EqualTo("Team1"));
        }

        [Test]
        public void TestRemoveTeam_TeamNotFound() {
            League league = new League();
            league.AddTeam(new Team("Team1"));

            var result = league.RemoveTeam("Team2");

            Assert.That(result, Is.False);
            Assert.That(league.Teams.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestPlayMatch_SuccessfulWin()
        {
            League league = new League();
            Team homeTeam = new Team("HomeTeam");
            Team awayTeam = new Team("AwayTeam");
            league.AddTeam(homeTeam);
            league.AddTeam(awayTeam);

            league.PlayMatch("HomeTeam", "AwayTeam", 3, 1);

            Assert.That(homeTeam.Wins, Is.EqualTo(1));
            Assert.That(homeTeam.Loses, Is.EqualTo(0));
            Assert.That(awayTeam.Wins, Is.EqualTo(0));
            Assert.That(awayTeam.Loses, Is.EqualTo(1));
        }

        [Test]
        public void TestPlayMatch_SuccessfulWinGuest()
        {
            League league = new League();
            Team homeTeam = new Team("HomeTeam");
            Team awayTeam = new Team("AwayTeam");
            league.AddTeam(homeTeam);
            league.AddTeam(awayTeam);

            league.PlayMatch("HomeTeam", "AwayTeam",1, 2);

            Assert.That(homeTeam.Wins, Is.EqualTo(0));
            Assert.That(homeTeam.Loses, Is.EqualTo(1));
            Assert.That(awayTeam.Wins, Is.EqualTo(1));
            Assert.That(awayTeam.Loses, Is.EqualTo(0));
        }

        [Test]
        public void TestPlayMatch_Draw()
        {
            League league = new League();
            Team homeTeam = new Team("HomeTeam");
            Team awayTeam = new Team("AwayTeam");
            league.AddTeam(homeTeam);
            league.AddTeam(awayTeam);

            league.PlayMatch("HomeTeam", "AwayTeam", 2, 2);

            Assert.That(homeTeam.Draws, Is.EqualTo(1));
            Assert.That(awayTeam.Draws, Is.EqualTo(1));
            Assert.That(homeTeam.Wins, Is.EqualTo(0));
            Assert.That(homeTeam.Loses, Is.EqualTo(0));
            Assert.That(awayTeam.Wins, Is.EqualTo(0));
            Assert.That(awayTeam.Loses, Is.EqualTo(0));
        }

        [Test]
        public void TestPlayMatch_TeamNotFound()
        {
            League league = new League();
            league.AddTeam(new Team("Team1"));

            var ex = Assert.Throws<InvalidOperationException>(() => league.PlayMatch("Team1", "NonExistentTeam", 1, 0));
            Assert.That(ex.Message, Is.EqualTo("One or both teams do not exist."));
        }

        [Test]
        public void TestGetTeamInfo_Success()
        {
            League league = new League();
            Team team = new Team("Team1");
            league.AddTeam(team);

            string teamInfo = league.GetTeamInfo("Team1");

            Assert.That(teamInfo, Is.EqualTo("Team1 - 0 points (0W 0D 0L)"));
        }

        [Test]
        public void TestGetTeamInfo_TeamNotFound()
        {
            League league = new League();

            var ex = Assert.Throws<InvalidOperationException>(() => league.GetTeamInfo("NonExistentTeam"));
            Assert.That(ex.Message, Is.EqualTo("Team does not exist."));
        }

        [Test]
        public void TestPlayMatch_TeamNotInLeague()
        {
            League league = new League();
            league.AddTeam(new Team("Team1"));

            var ex = Assert.Throws<InvalidOperationException>(() => league.PlayMatch("Team1", "Team2", 1, 0));
            Assert.That(ex.Message, Is.EqualTo("One or both teams do not exist."));
        }

    }
}